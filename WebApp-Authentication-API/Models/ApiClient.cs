namespace WebApp_Authentication_API.Models
{
    public class ApiClient
    {
        public Guid? AppId { get; set; }

        public string? Role { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }
    }
}
