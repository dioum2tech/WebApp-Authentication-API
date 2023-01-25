using System.Security.Claims;
using WebApp_Authentication_API.Models.StaticValues;

namespace WebApp_Authentication_API.Helpers.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Get the user unique identifier from the Azure claim <see cref="AzureClaimTypes.ObjectIdentifier"/>
        /// </summary>
        /// <param name="claimsPrincipal">User instance</param>
        public static Guid GetUniqueId(this ClaimsPrincipal claimsPrincipal)
        {
            var claimValue = claimsPrincipal?.FindFirstValue(AzureClaimTypes.ObjectIdentifier);
            _ = Guid.TryParse(claimValue, out var uniqueId);
            return uniqueId;
        }

        /// <summary>
        /// Get the user email from the claim <see cref="ClaimTypes.Email"/>
        /// </summary>
        /// <param name="claimsPrincipal">User instance</param>
        /// <returns>Returns the id or null if the claim has no value</returns>
        public static string? GetEmail(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.FindFirstValue(ClaimTypes.Email);
        }

        /// <summary>
        /// Get the user name from the claim <see cref="ClaimTypes.Name"/>
        /// </summary>
        /// <param name="claimsPrincipal">User instance</param>
        /// <returns>Returns the id or null if the claim has no value</returns>
        public static string? GetName(this ClaimsPrincipal claimsPrincipal)
        {
            return $"{claimsPrincipal?.FindFirstValue(ClaimTypes.GivenName)} {claimsPrincipal?.FindFirstValue(ClaimTypes.Surname)}";
        }

        /// <summary>
        /// Get the app id from the claims <see cref="AzureClaimTypes.AppId"/>
        /// </summary>
        /// <param name="claimsPrincipal">App instance</param>
        /// <returns>Returns the id or null if the claim has no value</returns>
        public static Guid? GetAppId(this ClaimsPrincipal claimsPrincipal)
        {
            string? claimValue = claimsPrincipal?.FindFirstValue("appid");
            _ = Guid.TryParse(claimValue, out Guid appId);
            return appId;
        }

        /// <summary>
        /// Get the role from the claims <see cref="ClaimTypes.Role"/>
        /// </summary>
        /// <param name="claimsPrincipal">App instance</param>
        /// <returns>Returns the roles or null if the claim has no value</returns>
        public static string? GetRole(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.FindFirstValue(ClaimTypes.Role);
        }
    }
}
