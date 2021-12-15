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

        public StudentController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(CreateStudentDto model)
        {
            int result = await _classService.CreateStudent(model);
            return Ok(new OkResult<int>(result, "Pomyślnie utworzono nowego ucznia"));
        }
    }
}