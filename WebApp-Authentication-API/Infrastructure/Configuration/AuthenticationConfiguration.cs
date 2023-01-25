using Microsoft.Identity.Web;
using JwtDefaults = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults;

namespace WebApp_Authentication_API.Infrastructure.Configuration
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
            // Add services to the container.
            services
                .AddAuthentication(JwtDefaults.AuthenticationScheme)
                    .AddMicrosoftIdentityWebApi(configuration, Constants.AzureAd);

            return services;
        }
    }
}
