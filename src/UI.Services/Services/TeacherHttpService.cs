using Shared.Dto.CreateAvailabilityDto;
using Shared.Dto.CreateTeacherDto;
using Shared.Responses;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Services.Interfaces;
using UI.Services.Models;

namespace UI.Services.Services
{
    public class TeacherHttpService : ITeacherHttpService
    {
        private readonly IHttpService _httpService;

        public TeacherHttpService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task CreateTeachersWithStudents(List<TeacherModel> models)
        {
            foreach (TeacherModel teacher in models)
            {
                var createTeacherDto = new CreateTeacherDto
                { FirstName = teacher.imie, LastName = teacher.nazwisko, HoursAvailability = teacher.ilosc_godzin };
                var teacherResult = await _httpService.Post<OkResult<int>>("api/teacher", createTeacherDto);
                var availabilities = await HandleAvailabilities(teacher.dostepnoscArr, teacherResult.Value);
                foreach (var availabilityDto in availabilities)
                {
                    var availabilityResult = await _httpService.Post<OkResult<int>>("api/availability", availabilityDto);
                }
            }
        }

        public async Task<IEnumerable<TeacherVm>> GetAllTeachersFromTimetable()
        {
            var result = await _httpService.Get<OkResult<IEnumerable<TeacherVm>>>
                ($"api/teacher/getall");
            return result.Value;
        }

        public async Task<int> GetTeachersCount()
        {
            var result = await _httpService.Get<OkResult<int>>("api/teacher/getcount");
            return result.Value;
        }

        private Task<List<CreateAvailabilityDto>> HandleAvailabilities(char[][] values, int teacherId)
        {
            const int startsAtInit = 8;
            List<CreateAvailabilityDto> result = new List<CreateAvailabilityDto>();

            for (int i = 0; i < values.Length - 1; i++)
            {
                for (int j = 0; j < values[i].Length - 1; j++)
                {
                    if (values[i][j] == '0')
                    {
                        result.Add(new CreateAvailabilityDto
                        {
                            DayOfWeek = MatchDayOfWeekByNumber(j),
                            StartsAt = i + startsAtInit,
                            EndsAt = i + startsAtInit + 1,
                            TeacherId = teacherId
                        });
                    }
                }
            }

            return Task.FromResult(result);
        }

        private string MatchDayOfWeekByNumber(int numberOfWeek) => numberOfWeek switch
        {
            0 => "Poniedziałek",
            1 => "Wtorek",
            2 => "Środa",
            3 => "Czwartek",
            4 => "Piątek",
            _ => "Błąd"
        };
    }
}