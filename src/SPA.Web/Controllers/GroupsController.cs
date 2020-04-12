using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Google.Apis.Auth;
using System.Linq;
using SPA.Web.Models;
using Microsoft.AspNetCore.Authorization;
using SPA.Web.Services;

namespace SPA.Web.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpPut("add/{id}")]
        public async Task<IActionResult> Add(int id)
        {
            var userId = HttpContext.GetUserId();
            if(userId == 0) return BadRequest();
            var result = await _groupService.AddUserToGroup(id, userId);
            if(result) return NoContent();
            return BadRequest();
        }

        [HttpDelete("remove/{groupId}")]
        public async Task<IActionResult> Remove(int id)
        {
            var userId = HttpContext.GetUserId();
            if(userId == 0) return BadRequest();
            var result = await _groupService.RemoveUserFromGroup(id, userId);
            if(result) return NoContent();
            return BadRequest();
        }
    }
}