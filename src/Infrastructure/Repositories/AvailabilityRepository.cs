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
    public class AvailabilityRepository : EfRepository<Availability>, IAvailabilityRepository
    {
        public AvailabilityRepository(ApplicationDbContext context) : base(context)
        {        
        }

        public Task DeleteAllTeacherAvailabilities(int teacherId)
        {
            var availabilities = _context.Availabilities.Where(x => x.TeacherId == teacherId);
            _context.Availabilities.RemoveRange(availabilities);
            return Task.CompletedTask;
        }
    }
}
