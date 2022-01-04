using Shared.Dto.CreateTeacherDto;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Services.Models;

namespace UI.Services.Interfaces
{
    public interface ITeacherHttpService
    {
        Task CreateTeacherWithAvailabilities(TeacherModel model);
        Task DeleteTeacher(int teacherId);
        Task<IEnumerable<TeacherVm>> GetAllTeachersFromTimetable();

        Task<int> GetTeachersCount();
    }
}