using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto.CreateTimetableDto;
using Shared.Responses;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TimetableController : ControllerBase
    {
        private readonly ITimetableService _timetableService;

        public TimetableController(ITimetableService timetableService)
        {
            _timetableService = timetableService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTimetableDto model)
        {
            var result = await _timetableService.CreateTimetable(model);
            return Ok(new OkResult<int>(result, "Pomyślnie utworzono nowy plan lekcji"));
        }

        [HttpPatch("changephase/{timetableId}")]
        public async Task<IActionResult> ChangePhase([FromRoute] int timetableId, [FromQuery] int phaseNumber)
        {
            await _timetableService.ChangePhaseNumber(timetableId, phaseNumber);
            return Ok(new Shared.Responses.OkResult("Pomyślnie zmieniono numer etapu"));
        }
    }
}