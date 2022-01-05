using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Services.Interfaces;

namespace UI.Services.Services
{
    public class StudentHttpService : IStudentHttpService
    {
        private readonly IHttpService _httpService;

        public StudentHttpService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<int> GetStudentsCount()
        {
            var result = await _httpService.Get<OkResult<int>>("api/student/getcount");
            return result.Value;
        }

        public async Task DeleteStudent(int studentId)
        {
            await _httpService.Delete<OkResult>($"api/student?studentId={studentId}", null);
        }
    }
}