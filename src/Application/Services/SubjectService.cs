using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Shared.Dto.CreateSubjectDto;
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

        public SubjectService(IMapper mapper, ISubjectRepository subjectRepository)
        {
            _mapper = mapper;
            _subjectRepository = subjectRepository;
        }

        public async Task<int> CreateSubject(CreateSubjectDto createSubjectDto)
        {
            var subject = _mapper.Map<Subject>(createSubjectDto);
            await _subjectRepository.AddAsync(subject);
            return subject.Id;
        }

        public async Task<int> GetTeachersCount(int timetableId)
        {
            int count = await _subjectRepository.GetCount(t => t.TimetableId == timetableId);
            return count;
        }
    }
}