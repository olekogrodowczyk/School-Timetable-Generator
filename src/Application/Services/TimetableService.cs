using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
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
            , IUserContextService userContextService)
        {
            _timetableRepository = timetableRepository;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public async Task<int> CreateTimetable()
        {
            int loggedUserId = _userContextService.GetUserId ?? 0;
            var timetable = new TimeTable();
            timetable.CreatorId = loggedUserId;
            await _timetableRepository.AddAsync(timetable);
            return timetable.Id;
        }

        public async Task ChangePhaseNumber(int timetableId, int phaseNumber)
        {
            var timetable = await _timetableRepository.GetByIdAsync(timetableId);
            if (timetable == null) { throw new NotFoundException($"Timetable with id: {timetableId} hasn't been found"); }
            int currentPhaseNumber = timetable.Id;
            timetable.CurrentPhase = phaseNumber;
            await _timetableRepository.UpdateAsync(timetable);
        }

        public async Task<int> GetCurrentPhase(int timetableId)
        {
            var timetable = await _timetableRepository.GetByIdAsync(timetableId);
            if (timetable == null) { throw new NotFoundException($"Timetable with id: {timetableId} hasn't been found"); }
            return timetable.CurrentPhase;
        }
    }
}