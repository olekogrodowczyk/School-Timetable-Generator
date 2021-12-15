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
        Task ChangePhaseNumber(int timetableId, int phaseNumber);
        Task<int> CreateTimetable(CreateTimetableDto model);
        Task<int> GetCurrentPhase(int timetableId);
    }
}
