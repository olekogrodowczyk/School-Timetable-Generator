using Application.Exceptions;
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
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUserContextService _userContextService;

        public UserService(IMapper mapper, IUserRepository userRepository, IUserContextService userContextService)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _userContextService = userContextService;
        }

        public async Task<int> GetCurrentActiveTimetable()
        {
            int currentActiveTimetableId = await _userRepository.GetCurrentActiveTimetable();
            if (currentActiveTimetableId == 0) { throw new BadRequestException("User doesn't have an active timetable"); }
            return currentActiveTimetableId;
        }
    }
}