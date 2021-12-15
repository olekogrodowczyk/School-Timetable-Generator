using Shared.Dto.CreateClassDto;
using Shared.Dto.CreateStudentDto;
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
    public class ClassHttpService : IClassHttpService
    {
        private readonly IHttpService _httpService;

        public ClassHttpService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task CreateClasses(List<ClassModel> models)
        {
            foreach (ClassModel model in models)
            {
                CreateClassDto createClassDto = new CreateClassDto { Name = model.name, TeacherName = model.teacher };
                var result = await _httpService.Post<OkResult<int>>("api/class", createClassDto);
                foreach (StudentModel student in model.studentsArr)
                {
                    CreateStudentDto createStudentDto =
                    new CreateStudentDto { FirstName = student.imie, LastName = student.nazwisko, ClassId = result.Value };
                    await _httpService.Post<OkResult<int>>("api/student", createStudentDto);
                }
            }
        }

        public async Task<IEnumerable<string>> GetAllClassessNames()
        {
            var result = await _httpService.Get<OkResult<IEnumerable<string>>>("api/class/getallnames");
            return result.Value;
        }

        public async Task<IEnumerable<StudentVm>> GetAllStudentsFromClass(string className)
        {
            var result = await _httpService.Get<OkResult<IEnumerable<StudentVm>>>
                ($"api/class/getstudentsbyclassname?name={className}");
            return result.Value;
        }

        public async Task<int> GetClassessCount()
        {
            var result = await _httpService.Get<OkResult<int>>("api/class/getcount");
            return result.Value;
        }
    }
}