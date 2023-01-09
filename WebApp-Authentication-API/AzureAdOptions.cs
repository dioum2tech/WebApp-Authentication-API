namespace WebApp_Authentication_API
{
    public class AzureAdOptions
    {
        public string? TenantId { get; set; }

        public string? ClientId { get; set; }

        public string? ClientSecret { get; set; }

        public string? CallbackPath { get; set; }

        public string? Instance { get; set; }

        public string Authority => $"{Instance}/{TenantId}/v2.0";

        public string AuthorizationUrl => $"{Instance}/{TenantId}/oauth2/v2.0/authorize";

        public string TokenUrl => $"{Instance}/{TenantId}/oauth2/token";
    }
}
