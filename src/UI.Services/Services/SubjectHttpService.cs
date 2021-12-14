using Shared.Dto.CreateGroupDto;
using Shared.Dto.CreateSubjectDto;
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
    public class SubjectHttpService : ISubjectHttpService
    {
        private readonly IHttpService _httpService;

        public SubjectHttpService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task AddSubjectsWithGroups(List<SubjectModel> subjectsWithGroups, int timetableId, string className)
        {
            foreach (var subjectModel in subjectsWithGroups)
            {
                var subjectResult = await _httpService.Post<OkResult<int>>("api/subject",
                    new CreateSubjectDto { Name = subjectModel.name, TimetableId = timetableId });
                foreach (var group in subjectModel.groupSubjectList)
                {
                    CreateGroupDto createGroupDto = new CreateGroupDto
                    {
                        Name = group.name,
                        SubjectName = subjectModel.name,
                        ClassName = className,
                        NumberOfLessonsInWeek = int.Parse(group.hours),
                        TeacherName = group.teacher,
                        StudentIds = group.studentsIdArr.ToList(),
                        TimetableId = timetableId
                    };
                    var groupResult = await _httpService.Post<OkResult<int>>("api/group", createGroupDto);
                }
            }
        }
    }
}