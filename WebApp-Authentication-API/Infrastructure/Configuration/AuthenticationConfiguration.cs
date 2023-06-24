using Microsoft.Identity.Web;
using Jwt = Microsoft.AspNetCore.Authentication.JwtBearer;
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
            services
                .AddAuthentication(JwtDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(configuration, "Azure:AD");

            //services.Configure<Jwt.JwtBearerOptions>(JwtDefaults.AuthenticationScheme, options =>
            //{
            //    options.Events = new Jwt.JwtBearerEvents
            //    {
            //        // Handle the token validated event
            //        OnTokenValidated = OnTokenValidated
            //    };
            //});

            return services;
        }

        /// <summary>
        /// Called after the auth token is validated.
        /// </summary>
        //private static async Task OnTokenValidated(Jwt.TokenValidatedContext context)
        //{
        //    var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();

        //    // Get user email from claims
        //    var uniqueId = context.Principal?.GetUniqueId();
        //    var email = context.Principal?.GetEmail();
        //    var name = context.Principal?.GetName();

        //    if (uniqueId is null)
        //    {
        //        var unknownUniqueIdentifierException = new UnknownUniqueIdentifierAuthenticationException("User does not have object identifier");
        //        context.Fail(unknownUniqueIdentifierException);
        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(email))
        //        {
        //            var unknownMailException = new UnknownEmailAuthenticationException($"User with object identifier '{uniqueId}' does not have an email.");
        //            context.Fail(unknownMailException);
        //        }
        //        else if (string.IsNullOrEmpty(name))
        //        {
        //            var unknownNameException = new UnknownNameAuthenticationException($"User with object identifier '{uniqueId}' does not have a name.");
        //            context.Fail(unknownNameException);
        //        }
        //        else
        //        {
        //            // Check if user exists in database
        //            var user = await userService.GetUser(uniqueId.Value);

        //            var identity = GetUserClaimsIdentity(user);
        //            context.Principal?.AddIdentity(identity);
        //        }
        //    }
        //}

        //private static ClaimsIdentity GetUserClaimsIdentity(User? user)
        //{
        //    var claims = new List<Claim>();

        //    if (user is not null && user.IsActive)
        //    {
        //        var nameClaim = new Claim(ClaimTypes.Name, user.Name ?? "");
        //        claims.Add(nameClaim);

        //        var userIdClaim = new Claim(CustomClaimTypes.UserId, user.UserId.ToString("d"));
        //        claims.Add(userIdClaim);

        //        var isGlobalAdministratorClaim = new Claim(CustomClaimTypes.UserIsGlobalAdministrator, user.IsGlobalAdministrator.ToString());
        //        claims.Add(isGlobalAdministratorClaim);

        //        var permissionsClaim = new Claim(CustomClaimTypes.UserPermissions, JsonSerializer.Serialize(user.Permissions));
        //        claims.Add(permissionsClaim);
        //    }

        //    return new ClaimsIdentity(claims);
        //}
    }
}