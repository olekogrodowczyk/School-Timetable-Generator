using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Shared.Dto.CreateTeacherDto;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IAvailabilityRepository _availabilityRepository;

        public TeacherService(ITeacherRepository teacherRepository, IMapper mapper, IUserRepository userRepository
            ,IAvailabilityRepository availabilityRepository)
        {
            _teacherRepository = teacherRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _availabilityRepository = availabilityRepository;
        }

        public async Task<int> CreateTeacher(CreateTeacherDto model)
        {
            int activeTimetableId = await _userRepository.GetCurrentActiveTimetable();
            var teacher = _mapper.Map<Teacher>(model);
            teacher.TimetableId = activeTimetableId;
            await _teacherRepository.AddAsync(teacher);
            return teacher.Id;
        }

        public async Task<IEnumerable<TeacherVm>> GetAllTeachersFromTimetable()
        {
            int activeTimetableId = await _userRepository.GetCurrentActiveTimetable();
            await _availabilityRepository.GetAllAsync();
            var teachers = await _teacherRepository.GetWhereAsync(x => x.TimetableId == activeTimetableId);
            if (teachers == null) { throw new NotFoundException($"Timetable entity with id: {activeTimetableId} is not found"); }
            return _mapper.Map<IEnumerable<TeacherVm>>(teachers);
        }

        public async Task<int> GetTeachersCount()
        {
            int activeTimetableId = await _userRepository.GetCurrentActiveTimetable();
            int count = await _teacherRepository.GetCount(t => t.TimetableId == activeTimetableId);
            return count;
        }
    }
}