using Shared.Dto.CreateClassDto;
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
    }
}
