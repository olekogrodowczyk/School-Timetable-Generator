using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ClassRepository : EfRepository<Class>, IClassRepository
    {
        public ClassRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
