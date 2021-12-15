using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Shared.Dto.CreateAvailabilityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly IMapper _mapper;
        private readonly IAvailabilityRepository _availabilityRepository;
        private readonly IUserRepository _userRepository;

        public AvailabilityService(IMapper mapper, IAvailabilityRepository availabilityRepository, IUserRepository userRepository)
        {
            _mapper = mapper;
            _availabilityRepository = availabilityRepository;
            _userRepository = userRepository;
        }

        public async Task<int> CreateAvailability(CreateAvailabilityDto model)
        {
            int activeTimetableId = await _userRepository.GetCurrentActiveTimetable();
            var availability = _mapper.Map<Availability>(model);
            availability.TimetableId = activeTimetableId;
            await _availabilityRepository.AddAsync(availability);
            return availability.Id;
        }
    }
}