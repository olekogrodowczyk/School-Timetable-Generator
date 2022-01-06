using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto.CreateGroupDto;
using Shared.Responses;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGroupDto model)
        {
            int result = await _groupService.CreateGroup(model);
            return Ok(new OkResult<int>(result, "Pomyślnie dodano nową grupę"));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int groupId)
        {
            await _groupService.DeleteGroupWithAssignments(groupId);
            return Ok(new Shared.Responses.OkResult("Pomyślnie usunięto wybraną grupę"));
        }
    }
}