using Microsoft.Identity.Web;
using Jwt = Microsoft.AspNetCore.Authentication.JwtBearer;
using JwtDefaults = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults;

namespace WebApp_Authentication_API
{
    public static class AuthenticationConfiguration
    {
        /// <summary>
        /// Setup the Authentication service with custom OpenId parameters and custom claims
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="configuration">The <see cref="IConfiguration"/> instance used by the app.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var result = configuration.GetSection("AzureAd");

            services.AddMicrosoftIdentityWebApiAuthentication(configuration, "AzureAd");

            services.Configure<Jwt.JwtBearerOptions>(JwtDefaults.AuthenticationScheme, options =>
            {
                options.Events = new Jwt.JwtBearerEvents
                {
                    // Handle the token validated event
                    OnTokenValidated = OnTokenValidated
                };
            });

            return services;
        }

        /// <summary>
        /// Called after the auth token is validated.
        /// </summary>
        private static async Task OnTokenValidated(Jwt.TokenValidatedContext context)
        {
            // Get user email from claims
            var uniqueId = context.Principal?.GetUniqueId();
            var email = context.Principal?.GetEmail();
            var name = context.Principal?.GetName();

            if (uniqueId is null)
            {
                var unknownUniqueIdentifierException = new UnknownUniqueIdentifierAuthenticationException("User does not have object identifier");
                context.Fail(unknownUniqueIdentifierException);
            }
            else
            {
                if (string.IsNullOrEmpty(email))
                {
                    var unknownMailException = new UnknownEmailAuthenticationException($"User with object identifier '{uniqueId}' does not have an email.");
                    context.Fail(unknownMailException);
                }
                else if (string.IsNullOrEmpty(name))
                {
                    var unknownNameException = new UnknownNameAuthenticationException($"User with object identifier '{uniqueId}' does not have a name.");
                    context.Fail(unknownNameException);
                }
            }
        }
    }
}
