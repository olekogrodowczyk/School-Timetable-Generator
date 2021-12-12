using Shared.Dto.CreateClassroomDto;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Services.Interfaces;
using UI.Services.Models;

namespace UI.Services.Services
{
    public class ClassroomHttpService : IClassroomHttpService
    {
        private readonly IHttpService _httpService;

        public ClassroomHttpService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task CreateClassrooms(List<ClassroomModel> models)
        {
            foreach (var model in models)
            {
                var dto = new CreateClassroomDto 
                { Code = model.kod, Name = model.nazwa, NumberOfSeats = int.Parse(model.ilosc_miejsc) };
                var result = await _httpService.Post<OkResult<int>>("api/classroom", dto);
            }
        }
    }
}
