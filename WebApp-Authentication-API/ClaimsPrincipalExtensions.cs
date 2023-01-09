using System.Security.Claims;
using System.Text.Json;
using WebApp_Authentication_API;

namespace WebApp_Authentication_API
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
    }
}
