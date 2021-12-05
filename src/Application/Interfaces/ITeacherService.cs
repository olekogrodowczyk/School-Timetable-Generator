using Shared.Dto.CreateTeacherDto;
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
    }
}
