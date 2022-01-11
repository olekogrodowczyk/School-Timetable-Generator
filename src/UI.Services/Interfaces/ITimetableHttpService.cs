using Shared.Responses;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Services.Interfaces
{
    public interface ITimetableHttpService
    {
        Task<int> CreateTimetable();
        Task Generate();
        Task<IEnumerable<TimetableOutcomeVm>> GetAlgorithmOutcome(int timetableId);
        Task<IEnumerable<TimetableVm>> GetGeneratedPlans();
    }
}