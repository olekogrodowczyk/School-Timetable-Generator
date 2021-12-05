using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Shared.Dto.CreateTimetableDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TimetableService : ITimetableService
    {
        private readonly ITimetableRepository _timetableRepository;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public TimetableService(ITimetableRepository timetableRepository, IMapper mapper
            ,IUserContextService userContextService)
        {
            _timetableRepository = timetableRepository;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public async Task<int> CreateTimetable(CreateTimetableDto model)
        {
            int loggedUserId = _userContextService.GetUserId ?? 0;
            var timetable = _mapper.Map<TimeTable>(model);
            timetable.CreatorId = loggedUserId;
            await _timetableRepository.AddAsync(timetable);
            return timetable.Id;
        }
    }
}
