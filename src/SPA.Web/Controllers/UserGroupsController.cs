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
using Microsoft.AspNetCore.Http;

namespace SPA.Web.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class UserGroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public UserGroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpPut("join/{id}")]
        public async Task<IActionResult> Join([FromRoute]int id)
        {
            var userId = GetUserId(HttpContext);
            if(userId == 0) return BadRequest();
            await _groupService.AddUserToGroup(id, userId);
            return NoContent();
        }

        //TODO Validation for request
        [HttpDelete("remove")]
        public async Task<IActionResult> Remove([FromBody]RemoveUserFromGroupRequest model)
        {
            await _groupService.RemoveUserFromGroup(model.GroupId, model.UserId);
            return NoContent();
        }

        private int GetUserId(HttpContext context)
        {
            var user = context.User;
            var userIdClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userIdClaim)) return 0;
            return int.Parse(userIdClaim);
        }
    }
}
