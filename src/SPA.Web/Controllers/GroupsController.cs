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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GroupCreateOrUpdateRequest model)
        {
            var id = await _groupService.CreateGroup(model.Name, model.Description);
            return Created("", new { id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute]int id, [FromBody] GroupCreateOrUpdateRequest model)
        {
            await _groupService.UpdateGroup(id, model.Name, model.Description);
            return NoContent();
        }
    }
}