using Shared.Dto.CreateClassroomDto;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Services.Models;

namespace UI.Services.Interfaces
{
    public interface IClassroomHttpService
    {
        Task CreateClassroom(ClassroomModel model);
        Task CreateClassrooms(List<ClassroomModel> models);
        Task DeleteClassroom(int classroomId);
        Task<IEnumerable<ClassroomVm>> GetAllClassroomsCreated();
        Task<int> GetClassroomsCount();
        Task UpdateClassroom(ClassroomModel model);
    }
}