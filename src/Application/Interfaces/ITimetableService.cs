using Shared.Dto.CreateTimetableDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITimetableService
    {
        Task<int> CreateTimetable(CreateTimetableDto model);
    }
}
