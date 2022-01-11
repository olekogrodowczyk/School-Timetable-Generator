using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto.ChangePhaseDto;
using Shared.Responses;
using Shared.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TimetableController : ControllerBase
    {
        private readonly ITimetableService _timetableService;
        private readonly IAlgorithmService _algorithmService;

        public TimetableController(ITimetableService timetableService, IAlgorithmService algorithmService)
        {
            _timetableService = timetableService;
            _algorithmService = algorithmService;
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var result = await _timetableService.CreateTimetable();
            return Ok(new OkResult<int>(result, "Pomyślnie utworzono nowy plan lekcji"));
        }

        [HttpPatch("changephase/{timetableId}")]
        public async Task<IActionResult> ChangePhase([FromRoute] int timetableId, [FromBody] ChangePhaseDto phaseNumber)
        {
            await _timetableService.ChangePhaseNumber(timetableId, phaseNumber.PhaseNumber);
            return Ok(new Shared.Responses.OkResult("Pomyślnie zmieniono numer etapu"));
        }

        [HttpGet("getcurrentphase/{timetableId}")]
        public async Task<IActionResult> GetCurrentPhase([FromRoute] int timetableId)
        {
            int result = await _timetableService.GetCurrentPhase(timetableId);
            return Ok(new Shared.Responses.OkResult<int>(result, "Pomyślnie zwrócono obecny etap"));
        }

        [HttpPost("generate")]
        public async Task<IActionResult> Generate()
        {
            await _algorithmService.Init();
            return Ok(new Shared.Responses.OkResult("Udało się"));
        }

        [AllowAnonymous]
        [HttpGet("getoutcome/{timetableId}")]
        public async Task<IActionResult> GetTimetableGeneretingOutcome([FromRoute]int timetableId)
        {
            var result = await _timetableService.GetTimetableGeneretingOutcome(timetableId);
            return Ok(new OkResult<IEnumerable<TimetableOutcomeVm>>(result, "Pomyślnie zwrócono wynik działania algorytmu"));
        }
    }
}