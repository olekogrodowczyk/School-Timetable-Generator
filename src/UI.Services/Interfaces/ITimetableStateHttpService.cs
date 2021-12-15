using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Services.Interfaces
{
    public interface ITimetableStateHttpService
    {
        Task<int> GetCurrentPhase(int timetableId);

        Task<int> GetCurrentTimetable();
    }
}