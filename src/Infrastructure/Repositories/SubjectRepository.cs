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

        public async Task<IEnumerable<Subject>> GetAllSubjectByTimetableIdWithJoins(int timetableId, string className)
        {
            await _context.Assignments.ToListAsync();
            await _context.Groups.ToListAsync();
            await _context.Classess.ToListAsync();
            var subjects = _context.Subjects
                .Include(x => x.Groups)
                .ThenInclude(x => x.Teacher)
                .Include(x => x.Groups)
                .ThenInclude(x => x.Assignments)
                .ThenInclude(x => x.Student)
                .Include(x=>x.Groups)
                .ThenInclude(x=>x.Class)
                .Where(x => x.TimetableId == timetableId && x.Groups.All(group=>group.Class.Name==className));
                
            return subjects;
        }
    }
}