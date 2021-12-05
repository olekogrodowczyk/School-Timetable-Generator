using Shared.Dto.CreateClassDto;
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
    public class ClassHttpService : IClassHttpService
    {
        private readonly IHttpService _httpService;

        public ClassHttpService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task CreateClass(List<ClassModel> model)
        {
            var classesWithoutCollection = model.Select(x => x.name).ToList();
            foreach (var item in classesWithoutCollection)
            {
                CreateClassDto createClassDto = new CreateClassDto { Name = item, TimetableId = 1, TeacherId = 1 };
                await _httpService.Post<OkResult<int>>("api/class", createClassDto);
            }
        }
    }
}
