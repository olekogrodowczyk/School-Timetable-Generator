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

        public AvailabilityService(IMapper mapper, IAvailabilityRepository availabilityRepository)
        {
            _mapper = mapper;
            _availabilityRepository = availabilityRepository;
        }

        public async Task<int> CreateAvailability(CreateAvailabilityDto model)
        {
            var availability = _mapper.Map<Availability>(model);
            await _availabilityRepository.AddAsync(availability);
            return availability.Id;
        }

    }
}
