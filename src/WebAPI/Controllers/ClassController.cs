using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto.CreateClassDto;
using Shared.Dto.CreateStudentDto;
using Shared.Responses;
using Shared.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateClass(CreateClassDto model)
        {
            int result = await _classService.CreateClass(model);
            return Ok(new OkResult<int>(result, "Pomyślnie utworzono nową klasę"));
        }

        [HttpGet("getallnames")]
        public async Task<IActionResult> GetAllClassessNames([FromQuery] int timetableId)
        {
            var result = await _classService.GetAllClassessNames(timetableId);
            return Ok(new OkResult<IEnumerable<string>>(result, "Pomyślnie zwrócono nazwy klas"));
        }

        [HttpGet("getbyname")]
        public async Task<IActionResult> GetClassByName([FromQuery] string name)
        {
            var result = await _classService.GetClassByName(name);
            return Ok(new OkResult<ClassVm>(result, "Pomyślnie zwrócono klasę"));
        }

        [HttpGet("getstudentsbyclassname")]
        public async Task<IActionResult> GetStudentsFromClass([FromQuery] string name)
        {
            var result = await _classService.GetStudentsFromGroup(name);
            return Ok(new OkResult<IEnumerable<StudentVm>>(result, "Pomyślnie zwrócono uczniów"));
        }
    }
}