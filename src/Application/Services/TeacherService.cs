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

        public TeacherService(ITeacherRepository teacherRepository, IMapper mapper)
        {
            _teacherRepository = teacherRepository;
            _mapper = mapper;
        }

        public async Task<int> CreateTeacher(CreateTeacherDto model)
        {
            var teacher = _mapper.Map<Teacher>(model);
            await _teacherRepository.AddAsync(teacher);
            return teacher.Id;
        }

        public async Task<IEnumerable<TeacherVm>> GetAllTeachersFromTimetable(int timetableId)
        {
            var teachers = await _teacherRepository.GetWhereAsync(x => x.TimetableId == timetableId);
            if (teachers == null) { throw new NotFoundException($"Timetable entity with id: {timetableId} is not found"); }
            return _mapper.Map<IEnumerable<TeacherVm>>(teachers);
        }
    }
}