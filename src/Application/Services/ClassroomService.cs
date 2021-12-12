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

        public ClassroomService(IMapper mapper, IClassroomRepository classroomRepository)
        {
            _mapper = mapper;
            _classroomRepository = classroomRepository;
        }
        
        public async Task<int> CreateClassroom(CreateClassroomDto createClassroomDto)
        {
            var classroom = _mapper.Map<Classroom>(createClassroomDto);
            await _classroomRepository.AddAsync(classroom);
            return classroom.Id;
        }

    }
}
