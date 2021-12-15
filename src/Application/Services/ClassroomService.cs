using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Shared.Dto.CreateClassroomDto;
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
    }
}