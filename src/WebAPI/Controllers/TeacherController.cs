using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto.CreateTeacherDto;
using Shared.Responses;
using Shared.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTeacherDto model)
        {
            int result = await _teacherService.CreateTeacher(model);
            return Ok(new OkResult<int>(result, "Pomyślnie dodano nauczyciela"));
        }

        [HttpGet("getallfromtimetable")]
        public async Task<IActionResult> GetAllFromTimetable(int timetableId)
        {
            var result = await _teacherService.GetAllTeachersFromTimetable(timetableId);
            return Ok(new OkResult<IEnumerable<TeacherVm>>(result, "Pomyślnie zwrócono nauczycieli"));
        }

        [HttpGet("getcount")]
        public async Task<IActionResult> GetTeachersCount([FromQuery] int timetableId)
        {
            int result = await _teacherService.GetSubjectsCount(timetableId);
            return Ok(new OkResult<int>(result, "Pomyślnie zwrócono liczbę nauczycieli"));
        }
    }
}