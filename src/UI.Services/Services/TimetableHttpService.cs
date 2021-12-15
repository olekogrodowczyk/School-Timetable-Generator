using Shared.Dto.CreateTimetableDto;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Services.Interfaces;
using UI;

namespace UI.Services.Services
{
    public class TimetableHttpService : ITimetableHttpService
    {
        private readonly IHttpService _httpService;

        public TimetableHttpService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task CreateTimetable(CreateTimetableDto model)
        {
            await _httpService.Post<OkResult<int>>("api/timetable", model);
        }

        public async Task ChangeCurrentPhase(int phaseNumber)
        {
        }
    }
}