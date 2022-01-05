using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IStudentRepository _studentRepository;

        public StudentService(IMapper mapper, IUserRepository userRepository, IStudentRepository studentRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _studentRepository = studentRepository;
        }

        public async Task<int> GetStudentsCount()
        {
            int activeTimetableId = await _userRepository.GetCurrentActiveTimetable();
            int count = await _studentRepository.GetCount(t => t.TimetableId == activeTimetableId);
            return count;
        }

        public async Task DeleteStudent(int studentId)
        {
            await _studentRepository.DeleteAsync(studentId);
        }
    }
}