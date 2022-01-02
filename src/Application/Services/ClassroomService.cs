using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Shared.Dto;
using Shared.Dto.CreateClassroomDto;
using Shared.Dto.UpdateClassroom;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ClassroomService : IClassroomService
    {
        private readonly IMapper _mapper;
        private readonly IClassroomRepository _classroomRepository;
        private readonly IUserRepository _userRepository;

        public ClassroomService(IMapper mapper, IClassroomRepository classroomRepository, IUserRepository userRepository)
        {
            _mapper = mapper;
            _classroomRepository = classroomRepository;
            _userRepository = userRepository;
        }

        public async Task<int> CreateClassroom(CreateClassroomDto createClassroomDto)
        {
            int activeTimetableId = await _userRepository.GetCurrentActiveTimetable();
            var classroom = _mapper.Map<Classroom>(createClassroomDto);
            classroom.TimetableId = activeTimetableId;
            await _classroomRepository.AddAsync(classroom);
            return classroom.Id;
        }

        public async Task<int> GetClassroomsCount()
        {
            int activeTimetableId = await _userRepository.GetCurrentActiveTimetable();
            int count = await _classroomRepository.GetCount(c => c.TimetableId == activeTimetableId);
            return count;
        }

        public async Task<IEnumerable<ClassroomVm>> GetAllCreatedClassrooms()
        {
            int activeTimetableId = await _userRepository.GetCurrentActiveTimetable();
            var classrooms = await _classroomRepository.GetWhereAsync(x=>x.TimetableId == activeTimetableId);
            return _mapper.Map<IEnumerable<ClassroomVm>>(classrooms);
        }

        public async Task DeleteClassroom(int classroomId)
        {
            await _classroomRepository.DeleteAsync(classroomId);
        }

        public async Task UpdateClassroom(UpdateClassroomDto model)
        {
            int activeTimetableId = await _userRepository.GetCurrentActiveTimetable();
            var classroom = _mapper.Map<Classroom>(model);
            classroom.TimetableId=activeTimetableId;
            await _classroomRepository.UpdateAsync(classroom);
        }
    }
}