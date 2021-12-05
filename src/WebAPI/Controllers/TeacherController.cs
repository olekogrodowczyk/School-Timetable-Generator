using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto.CreateTeacherDto;
using Shared.Responses;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        public async Task<IActionResult> Create(CreateTeacherDto model)
        {
            int result = await _teacherService.CreateTeacher(model);
            return Ok(new OkResult<int>(result, "Pomyślnie dodano nauczyciela"));
        }
    }
}
