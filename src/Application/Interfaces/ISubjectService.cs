using Shared.Dto.CreateSubjectDto;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISubjectService
    {
        Task<int> CreateSubject(CreateSubjectDto createSubjectDto);
        Task DeleteSubject(int subjectId);
        Task<IEnumerable<SubjectVm>> GetAllSubjects();
        Task<int> GetTeachersCount(int timetableId);
    }
}