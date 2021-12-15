using Shared.Dto.CreateTimetableDto;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Services.Interfaces;
using UI;
using Shared.Dto.ChangePhaseDto;

namespace UI.Services.Services
{
    public class TimetableHttpService : ITimetableHttpService
    {
        private readonly IHttpService _httpService;

        public TimetableHttpService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<int> CreateTimetable(CreateTimetableDto model)
        {
            var result = await _httpService.Post<OkResult<int>>("api/timetable", model);
            return result.Value;
        }
    }
}