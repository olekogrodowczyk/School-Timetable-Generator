using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Shared.Dto.CreateSubjectDto;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly IMapper _mapper;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IUserRepository _userRepository;

        public SubjectService(IMapper mapper, ISubjectRepository subjectRepository, IUserRepository userRepository)
        {
            _mapper = mapper;
            _subjectRepository = subjectRepository;
            _userRepository = userRepository;
        }

        public async Task<int> CreateSubject(CreateSubjectDto createSubjectDto)
        {
            int activeTimetableId = await _userRepository.GetCurrentActiveTimetable();
            var subject = _mapper.Map<Subject>(createSubjectDto);
            subject.TimetableId = activeTimetableId;
            await _subjectRepository.AddAsync(subject);
            return subject.Id;
        }

        public async Task<int> GetTeachersCount(int timetableId)
        {
            int activeTimetableId = await _userRepository.GetCurrentActiveTimetable();
            int count = await _subjectRepository.GetCount(s => s.TimetableId == activeTimetableId);
            return count;
        }

        public async Task<IEnumerable<SubjectVm>> GetAllSubjects(string className)
        {
            int activeTimetableId = await _userRepository.GetCurrentActiveTimetable();
            var subjects = await _subjectRepository.GetAllSubjectByTimetableIdWithJoins(activeTimetableId, className);
            var subjectsVms = _mapper.Map<IEnumerable<SubjectVm>>(subjects);
            return subjectsVms;
        }

        public async Task DeleteSubject(int subjectId)
        {
            await _subjectRepository.DeleteAsync(subjectId);
        }
    }
}