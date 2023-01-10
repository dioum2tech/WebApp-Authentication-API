using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace WebApp_Authentication_API
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider apiVersionProvider;
        private readonly AzureAdOptions azureAdOptions;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider apiVersionProvider, IOptions<AzureAdOptions> azureAdOptions)
        {
            this.apiVersionProvider = apiVersionProvider;
            this.azureAdOptions = azureAdOptions.Value;
        }

        public void Configure(SwaggerGenOptions options)
        {
            // Add a document for each supported API version
            var apiVersions = apiVersionProvider?.ApiVersionDescriptions;
            if (apiVersions is object)
            {
                foreach (var version in apiVersions)
                {
                    options.SwaggerDoc(version.GroupName, BuildApiInfo(version));
                }
            }

            options.TagActionsBy(api =>
            {
                if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                {
                    CustomAttributeData? areaAttribute = controllerActionDescriptor.ControllerTypeInfo.CustomAttributes
                                                            .FirstOrDefault(attribute => attribute.AttributeType.Name == "AreaAttribute");
                    string? area = (string?)areaAttribute?.ConstructorArguments?
                                .FirstOrDefault().Value;

                    if (area is null)
                    {
                        return new[] { controllerActionDescriptor.ControllerName };
                    }
                    else
                    {
                        return new[] { area };
                    }

                }

                throw new InvalidOperationException("Unable to determine tag for endpoint.");
            });

            // Add the OpenId authentication settings
            options.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    ClientCredentials = new OpenApiOAuthFlow
                    {
                        TokenUrl = new Uri(azureAdOptions.TokenUrl),
                        Scopes = new Dictionary<string, string>
                        {
                            ["openid"] = "Grants access to the user's profile and connect using his Microsoft account",
                            [$"api://{azureAdOptions.ClientId}/.default"] = "ReadAccess"
                        }
                    }
                }
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "OAuth2" },
                    },
                    new List<string>()
                }
            });

            //// Use XML comments
            //var projectAssembly = Assembly.GetEntryAssembly();
            //if (projectAssembly is object)
            //{
            //    string xmlFile = $"{projectAssembly.GetName().Name}.xml";
            //    string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //    options.IncludeXmlComments(xmlPath);
            //}
        }

        private static OpenApiInfo BuildApiInfo(ApiVersionDescription apiVersion)
        {
            return new OpenApiInfo
            {
                Title = "WebApp Authentication API",
                Version = apiVersion.ApiVersion.ToString(),
                Description = apiVersion.IsDeprecated ? "This API version has been deprecated." : string.Empty,
            };
        }
    }
}
