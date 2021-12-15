using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto.CreateClassroomDto;
using Shared.Responses;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomController : ControllerBase
    {
        private readonly IClassroomService _classroomService;

        public ClassroomController(IClassroomService classroomService)
        {
            _classroomService = classroomService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClassroomDto model)
        {
            int result = await _classroomService.CreateClassroom(model);
            return Ok(new OkResult<int>(result, "Pomyślnie dodano nową salę"));
        }

        [HttpGet("getcount")]
        public async Task<IActionResult> GetCount()
        {
            int result = await _classroomService.GetClassroomsCount();
            return Ok(new OkResult<int>(result, "Pomyślnie zwrócono ilość sal"));
        }
    }
}