using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : EfRepository<User>, IUserRepository
    {
        private readonly IUserContextService _userContextService;

        public UserRepository(ApplicationDbContext context, IUserContextService userContextService) : base(context)
        {
            _userContextService = userContextService;
        }

        public async Task<int> GetCurrentActiveTimetable()
        {
            int loggedUserId = _userContextService.GetUserId ?? 0;
            if (loggedUserId == 0) { throw new BadRequestException("Unexpected error handled"); }
            var loggedUser = await _context.Users.SingleOrDefaultAsync(u => u.Id == loggedUserId);
            int currentActiveTimetableId = loggedUser.CurrentTimetableId ?? 0;
            return currentActiveTimetableId;
        }
    }
}