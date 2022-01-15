using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Services.Interfaces;
using UI;
using Shared.Dto.ChangePhaseDto;
using Shared.ViewModels;

namespace UI.Services.Services
{
    public class TimetableHttpService : ITimetableHttpService
    {
        private readonly IHttpService _httpService;

        public TimetableHttpService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<int> CreateTimetable()
        {
            var result = await _httpService.Post<OkResult<int>>("api/timetable", null);
            return result.Value;
        }

        public async Task Generate()
        {
            var result = await _httpService.Post<OkResult>("api/timetable/generate", null);
        }

        public async Task<IEnumerable<TimetableOutcomeVm>> GetAlgorithmOutcome(int timetableId)
        {
            var result = await _httpService.Get<OkResult<IEnumerable<TimetableOutcomeVm>>>
                ($"api/timetable/getoutcome/{timetableId}");
            return result.Value;
        }

        public async Task<IEnumerable<TimetableVm>> GetUserTimetables()
        {
            var result = await _httpService.Get<OkResult<IEnumerable<TimetableVm>>>("api/timetable/getusertimetables");
            return result.Value;
        }

        public async Task ChangeCurrentUserTimetable(int timetableId)
        {
            var result = await _httpService.Patch<OkResult>($"api/timetable/changecurrenttimetable?timetableId={timetableId}",null);
        }

        public async Task<int> GetCurrentUserTimetableId()
        {
            var result = await _httpService.Get<OkResult<int>>("api/timetable/getcurrenttimetable");
            return result.Value;
        }
    }
}