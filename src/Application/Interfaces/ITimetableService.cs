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
        Task<int> CreateTimetable();
        Task<int> GetCurrentPhase(int timetableId);
    }
}
