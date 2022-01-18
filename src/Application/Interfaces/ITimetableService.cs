using Shared.ViewModels;
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
        Task ChangeUserCurrentTimetable(int timetableId);
        Task<int> CreateTimetable();
        Task<int> GetCurrentPhase(int timetableId);
        Task<int> GetCurrentUserTimetableId();
        Task<IEnumerable<TimetableVm>> GetUserTimetables();
        Task<IEnumerable<TimetableOutcomeVm>> GetTimetableGeneretingOutcome(int timetableId);
    }
}
