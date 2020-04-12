using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Google.Apis.Auth;
using System.Linq;
using SPA.Web.Models;
using SPA.Web.Services;
using Microsoft.AspNetCore.Authorization;

namespace SPA.Web.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<string> Login([FromBody] GoogleLogin login) 
            => await _userService.GetAppToken(login.Token); 

        [Authorize]
        [HttpPut("join/{groupId}")]
        public async Task<IActionResult> Join(int groupId)
        {
            var user = HttpContext.User;
            var userIdClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userIdClaim)) return BadRequest();
            var userId = int.Parse(userIdClaim);
            await _userService.AddUserToGroup(groupId, userId);
            return NoContent();
        }
    }
}
