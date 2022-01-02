using Shared.Dto;
using Shared.Dto.CreateClassroomDto;
using Shared.Dto.UpdateClassroom;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IClassroomService
    {
        Task<int> CreateClassroom(CreateClassroomDto createClassroomDto);
        Task DeleteClassroom(int classroomId);
        Task<IEnumerable<ClassroomVm>> GetAllCreatedClassrooms();
        Task<int> GetClassroomsCount();
        Task UpdateClassroom(UpdateClassroomDto model);
    }
}