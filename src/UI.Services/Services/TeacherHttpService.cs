using Shared.Dto.CreateTeacherDto;
using Shared.Responses;
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
            foreach(TeacherModel teacher in models)
            {
                var createTeacherDto = new CreateTeacherDto { FirstName = teacher.imie, LastName = teacher.nazwisko };
                var result = await _httpService.Post<OkResult<int>>("api/teacher", createTeacherDto);
                
            }         
        }

    }
}
