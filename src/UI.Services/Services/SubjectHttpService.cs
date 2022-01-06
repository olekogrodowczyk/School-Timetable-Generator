using Shared.Dto.CreateGroupDto;
using Shared.Dto.CreateSubjectDto;
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
    public class SubjectHttpService : ISubjectHttpService
    {
        private readonly IHttpService _httpService;

        public SubjectHttpService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task AddSubjectWithGroups(SubjectModel model, string className)
        {
            var subjectResult = await _httpService.Post<OkResult<int>>("api/subject",
                    new CreateSubjectDto { Name = model.name });            
            foreach (var group in model.groupSubjectList)
            {
                if (group.name == "") { group.name = $"{className} {model.name}"; }
                CreateGroupDto createGroupDto = new CreateGroupDto
                {
                    Name = group.name.Trim(),
                    SubjectName = model.name.Trim(),
                    ClassName = className.Trim(),
                    NumberOfLessonsInWeek = int.Parse(group.hours.Trim()),
                    TeacherName = group.teacher.Trim(),
                    StudentIds = group.studentsIdArr.ToList(),
                };
                var groupResult = await _httpService.Post<OkResult<int>>("api/group", createGroupDto);
            }
        }

        public async Task<IEnumerable<SubjectVm>> GetAllSubjectsWithGroups()
        {
            var result = await _httpService.Get<OkResult<IEnumerable<SubjectVm>>>("api/subject/getall");
            return result.Value;
        }
    }
}