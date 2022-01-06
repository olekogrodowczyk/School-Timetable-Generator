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
    public class SubjectRepository : EfRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Subject>> GetAllSubjectByTimetableIdWithJoins(int timetableId)
        {
            await _context.Assignments.ToListAsync();
            await _context.Groups.ToListAsync();
            var subjects = _context.Subjects.Where(x => x.TimetableId == timetableId)
                .Include(x => x.Groups)
                .ThenInclude(x => x.Teacher)
                .Include(x => x.Groups)
                .ThenInclude(x => x.Assignments)
                .ThenInclude(x => x.Student);
            return subjects;
        }
    }
}