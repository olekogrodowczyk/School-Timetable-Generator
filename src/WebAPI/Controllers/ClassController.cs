using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto.CreateClassDto;
using Shared.Dto.CreateStudentDto;
using Shared.Responses;
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

        
    }
}
