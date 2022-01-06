using Shared.Dto.CreateClassroomDto;
using Shared.Dto.UpdateClassroom;
using Shared.Responses;
using Shared.ViewModels;
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

        public async Task CreateClassroom(ClassroomModel model)
        {
            var dto = new CreateClassroomDto
            { Code = model.kod.Trim(), Name = model.nazwa.Trim(), NumberOfSeats = int.Parse(model.ilosc_miejsc.Trim()) };
            var result = await _httpService.Post<OkResult<int>>("api/classroom", dto);
        }

        public async Task<int> GetClassroomsCount()
        {
            var result = await _httpService.Get<OkResult<int>>("api/classroom/getcount");
            return result.Value;
        }

        public async Task<IEnumerable<ClassroomVm>> GetAllClassroomsCreated()
        {
            var result = await _httpService.Get<OkResult<IEnumerable<ClassroomVm>>>("api/classroom/getallcreated");
            return result.Value;
        }

        public async Task DeleteClassroom(int classroomId)
        {
            await _httpService.Delete<OkResult>($"api/classroom?classroomId={classroomId}", null);
        }

        public async Task UpdateClassroom(ClassroomModel model)
        {
            var UpdateClassroomDto = new UpdateClassroomDto 
            { Id = model.id, Code = model.kod.Trim(), Name = model.nazwa.Trim(), NumberOfSeats = int.Parse(model.ilosc_miejsc.Trim()) };
            await _httpService.Put<OkResult>($"api/classroom", UpdateClassroomDto);
        }
    }
}