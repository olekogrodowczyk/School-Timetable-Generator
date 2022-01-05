using Shared.Dto.CreateClassDto;
using Shared.Dto.CreateStudentDto;
using Shared.Dto.UpdateClassDto;
using Shared.ViewModels;
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
        Task DeleteClass(int classId);
        Task<IEnumerable<ClassVm>> GetAllClassess();
        Task<IEnumerable<string>> GetAllClassessNames();

        Task<ClassVm> GetClassByName(string name);

        Task<int> GetClassessCount();

        Task<IEnumerable<StudentVm>> GetStudentsFromClass(string className);
        Task UpdateClass(UpdateClassDto model);
    }
}