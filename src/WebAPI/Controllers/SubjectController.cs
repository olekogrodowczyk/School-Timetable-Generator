﻿using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto.CreateSubjectDto;
using Shared.Responses;
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
    }
}