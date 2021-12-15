using Shared.Dto.CreateSubjectDto;
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

        Task<int> GetTeachersCount(int timetableId);
    }
}