using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Shared.Dto.CreateClassDto;
using Shared.Dto.CreateStudentDto;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ITimetableRepository _timetableRepository;
        private readonly IMapper _mapper;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUserRepository _userRepository;

        public ClassService(IClassRepository classRepository, IStudentRepository studentRepository
            , ITimetableRepository timetableRepository, IMapper mapper, ITeacherRepository teacherRepository
            , IUserRepository userRepository)
        {
            _classRepository = classRepository;
            _studentRepository = studentRepository;
            _timetableRepository = timetableRepository;
            _mapper = mapper;
            _teacherRepository = teacherRepository;
            _userRepository = userRepository;
        }

        public async Task<int> CreateClass(CreateClassDto model)
        {
            int activeTimetableId = await _userRepository.GetCurrentActiveTimetable();
            var classToAdd = _mapper.Map<Class>(model);
            classToAdd.TimetableId = activeTimetableId;
            await _classRepository.AddAsync(classToAdd);
            return classToAdd.Id;
        }

        public async Task<int> CreateStudent(CreateStudentDto model)
        {
            int activeTimetableId = await _userRepository.GetCurrentActiveTimetable();
            var student = _mapper.Map<Student>(model);
            student.TimetableId = activeTimetableId;
            await _studentRepository.AddAsync(student);
            return student.Id;
        }

        public async Task<IEnumerable<string>> GetAllClassessNames()
        {
            int activeTimetableId = await _userRepository.GetCurrentActiveTimetable();
            var result = await _classRepository.GetWhereAsync(x => x.TimetableId == activeTimetableId);
            return result.Select(x => x.Name);
        }

        public async Task<ClassVm> GetClassByName(string className)
        {
            await _teacherRepository.GetAllAsync();
            var classToMap = await _classRepository.SingleOrDefaultAsync(x => x.Name == className, x => x.Teacher);
            if (classToMap == null) { throw new NotFoundException("Class name couldn't be found"); }
            return _mapper.Map<ClassVm>(classToMap);
        }

        public async Task<IEnumerable<StudentVm>> GetStudentsFromClass(string className)
        {
            await _studentRepository.GetAllAsync();
            var classFound = await _classRepository.SingleOrDefaultAsync(x => x.Name == className, x => x.Students);
            if (classFound == null) { throw new NotFoundException("Class name couldn't be found"); }
            var result = _mapper.Map<IEnumerable<StudentVm>>(classFound.Students);
            return result;
        }

        public async Task<int> GetClassessCount()
        {
            int activeTimetableId = await _userRepository.GetCurrentActiveTimetable();
            int count = await _classRepository.GetCount(c => c.TimetableId == activeTimetableId);
            return count;
        }
    }
}