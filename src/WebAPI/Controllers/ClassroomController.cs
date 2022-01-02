using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto.CreateClassroomDto;
using Shared.Dto.UpdateClassroom;
using Shared.Responses;
using Shared.ViewModels;
using System.Collections.Generic;
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

        [HttpGet("getallcreated")]
        public async Task<IActionResult> GetAllCreated()
        {
            var result = await _classroomService.GetAllCreatedClassrooms();
            return Ok(new OkResult<IEnumerable<ClassroomVm>>(result, "Pomyślnie zwrócono sale"));
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

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateClassroomDto model)
        {
            await _classroomService.UpdateClassroom(model);
            return Ok(new Shared.Responses.OkResult("Pomyślnie zaktualizowano salę"));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int classroomId)
        {
            await _classroomService.DeleteClassroom(classroomId);
            return Ok(new Shared.Responses.OkResult("Pomyślnie usunięto salę"));
        }
    }
}