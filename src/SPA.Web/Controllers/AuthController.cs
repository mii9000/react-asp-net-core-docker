using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Google.Apis.Auth;
using System.Linq;

namespace SPA.Web.Controllers
{
    public class GoogleLogin
    {
        public string Token { get; set; }
    }

    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService jwtService;

        public AuthController(IJwtService jwtService)
        {
            this.jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<string> Login([FromBody] GoogleLogin login)
        {
            try
            {
                var result = await GoogleJsonWebSignature.ValidateAsync(login.Token);
                var repo = new Repository();
                var sh = repo.All();
                var s = jwtService.GetToken(result.Email, result.Issuer, result.Audience.ToString());
                return sh.ElementAt(0).title;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // [HttpPost]
        // public Task Logout()
        // {
                
        // }

    }
}
