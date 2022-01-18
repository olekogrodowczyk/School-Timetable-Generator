using Shared.Dto.CreateTeacherDto;
using Shared.Dto.UpdateTeacherDto;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITeacherService
    {
        Task<int> CreateTeacher(CreateTeacherDto model);
        Task DeleteTeacher(int teacherId);
        Task<IEnumerable<TeacherVm>> GetAllTeachersFromTimetable();

        Task<int> GetTeachersCount();
        Task<bool> TeacherExists(string firstName, string lastName);
        Task UpdateTeacher(UpdateTeacherDto model);
    }
}