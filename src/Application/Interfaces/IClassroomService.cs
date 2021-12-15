using Shared.Dto.CreateClassroomDto;
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

        Task<int> GetClassroomsCount();
    }
}