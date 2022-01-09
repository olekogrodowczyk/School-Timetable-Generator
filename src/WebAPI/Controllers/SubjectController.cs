using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto.CreateSubjectDto;
using Shared.Responses;
using Shared.ViewModels;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSubjectDto createSubjectDto)
        {
            int result = await _subjectService.CreateSubject(createSubjectDto);
            return Ok(new OkResult<int>(result, "Pomyślnie dodano nowy przedmiot"));
        }

        [HttpGet("getcount")]
        public async Task<IActionResult> GetTeachersCount([FromQuery] int timetableId)
        {
            int result = await _subjectService.GetTeachersCount(timetableId);
            return Ok(new OkResult<int>(result, "Pomyślnie liczbę przedmiotów"));
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllSubjectsWithGroups([FromQuery] string className)
        {
            var result = await _subjectService.GetAllSubjects(className);
            return Ok(new OkResult<IEnumerable<SubjectVm>>(result, "Pomyślnie zwrócono przedmioty"));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSubjectWithGroups([FromQuery] int subjectId)
        {
            await _subjectService.DeleteSubject(subjectId);
            return Ok(new Shared.Responses.OkResult("Pomyślnie usunięto wybrany przedmiot"));
        }
    }
}