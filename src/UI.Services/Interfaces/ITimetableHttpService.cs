using Shared.Dto.CreateTimetableDto;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Services.Interfaces
{
    public interface ITimetableHttpService
    {
        Task<int> CreateTimetable(CreateTimetableDto model);

        Task Generate();
    }
}