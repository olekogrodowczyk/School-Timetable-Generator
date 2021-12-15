using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto.CreateStudentDto;
using Shared.Responses;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IClassService _classService;
        private readonly IStudentService _studentService;

        public StudentController(IClassService classService, IStudentService studentService)
        {
            _classService = classService;
            _studentService = studentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(CreateStudentDto model)
        {
            int result = await _classService.CreateStudent(model);
            return Ok(new OkResult<int>(result, "Pomyślnie utworzono nowego ucznia"));
        }

        [HttpGet("getcount")]
        public async Task<IActionResult> GetStudentsCount()
        {
            int result = await _studentService.GetStudentsCount();
            return Ok(new OkResult<int>(result, "Pomyślnie zwrócono liczbę uczniów"));
        }
    }
}