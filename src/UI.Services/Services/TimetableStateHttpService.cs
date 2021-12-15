using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Services.Interfaces;

namespace UI.Services.Services
{
    public class TimetableStateHttpService : ITimetableStateHttpService
    {
        private readonly IHttpService _httpService;

        public TimetableStateHttpService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<int> GetCurrentPhase(int timetableId)
        {
            var result = await _httpService.Get<OkResult<int>>($"api/timetable/getcurrentphase/{timetableId}");
            return result.Value;
        }

        public async Task<int> GetCurrentTimetable()
        {
            var result = await _httpService.Get<OkResult<int>>("api/account/getcurrenttimetable");
            return result.Value;
        }
    }
}