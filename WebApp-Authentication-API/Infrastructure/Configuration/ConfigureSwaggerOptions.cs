using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApp_Authentication_API.Infrastructure.Options;

namespace WebApp_Authentication_API.Infrastructure.Configuration
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly AzureAdOptions azureAdOptions;

        public ConfigureSwaggerOptions(IOptions<AzureAdOptions> azureAdOptions)
        {
            this.azureAdOptions = azureAdOptions.Value;
        }

        public void Configure(SwaggerGenOptions options)
        {
            #region OAuth2 Implicit

            options.AddSecurityDefinition("OAuth2 Implicit", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Scheme = "Bearer",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Flows = new OpenApiOAuthFlows
                {
                    Implicit = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(azureAdOptions.AuthorizationUrl),
                        Scopes = new Dictionary<string, string>
                        {
                            { azureAdOptions.CustomScopeApi, azureAdOptions.CustomScopeApi },
                        }
                    },
                }
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Name = "OAuth2 Implicit",
                        In = ParameterLocation.Header,
                        Reference = new OpenApiReference
                        {
                            Id = "OAuth2 Implicit",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new List<string>()
                }
            });

            #endregion OAuth2 Implicit

            #region Bearer

            options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new List<string>()
                }
            });

            #endregion Bearer

            #region OAuth Client Credentials

            options.AddSecurityDefinition("OAuth Client Credentials", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    ClientCredentials = new OpenApiOAuthFlow
                    {
                        TokenUrl = new Uri(azureAdOptions.TokenUrl),
                        Scopes = new Dictionary<string, string>
                        {
                            { azureAdOptions.DefaultScopeApi, azureAdOptions.DefaultScopeApi },
                        }
                    }
                }
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Name = "OAuth Client Credentials",
                        In = ParameterLocation.Header,
                        Reference = new OpenApiReference
                        {
                            Id = "OAuth Client Credentials",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new List<string>()
                }
            });

            #endregion OAuth Client Credentials

            #region Authorization code

            options.AddSecurityDefinition("OAuth2 autorization code", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Scheme = "oauth2_auth_code",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(azureAdOptions.AuthorizationUrl),
                        TokenUrl = new Uri(azureAdOptions.TokenUrl),
                        Scopes = new Dictionary<string, string>
                        {
                            { "openid", "OpenID" },
                            { azureAdOptions.CustomScopeApi, "Your API Scope"  },
                        }
                    }
                }
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Reference = new OpenApiReference
                        {
                            Id = "OAuth2 autorization code",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new[] { "openid", azureAdOptions.CustomScopeApi }
                }
            });

            #endregion Authorization code
        }
    }
}