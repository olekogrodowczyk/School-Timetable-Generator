using Shared.Dto.CreateClassDto;
using Shared.Dto.CreateStudentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IClassService
    {
        Task<int> CreateClass(CreateClassDto model);
        Task<int> CreateStudent(CreateStudentDto model);
        Task<IEnumerable<string>> GetAllClassessNames(int timetableId);
    }
}
