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

        [HttpGet]
        public async Task<ActionResult<UserGroupsResponse>> Get()
        {
            var (UserId, Username) = HttpContext.GetUserClaims();
            if(UserId == 0) return BadRequest();
            return Ok(await _groupService.GetAllUserGroups(UserId, Username));
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Post([FromRoute]int id)
        {
            var (UserId, Username) = HttpContext.GetUserClaims();
            if(UserId == 0) return BadRequest();
            await _groupService.AddUserToGroup(id, UserId);
            return NoContent();
        }

        [HttpDelete("users/{userId}/groups/{groupId}")]
        public async Task<IActionResult> Delete([FromRoute]int userId, [FromRoute]int groupId)
        {
            await _groupService.RemoveUserFromGroup(groupId, userId);
            return NoContent();
        }
    }
}
