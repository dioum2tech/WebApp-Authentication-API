using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp_Authentication_API.Helpers.Extensions;
using WebApp_Authentication_API.Models;

namespace WebApp_Authentication_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/v{version:apiVersion}/[controller]")]
    //[Authorize(Roles = "DevOAuthApi.DevOAuthRole")]
    public class ApiClientController : ControllerBase
    {
        [HttpGet]
        public ApiClient Get()
        {
            Guid? appId = User.GetAppId();
            string? role = User.GetRole();
            string? name = User.GetName();
            string? email = User.GetEmail();

            ApiClient me = new()
            {
                AppId = appId,
                Role = role,
                Name = name,
                Email = email,
            };

            return me;
        }
    }
}