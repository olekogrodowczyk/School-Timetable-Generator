using Domain.Entities;
using Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI
{
    public class Seeder
    {
        private readonly ApplicationDbContext _context;

        public Seeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if(_context.Database.CanConnect())
            {
                if(!_context.Roles.Any())
                {
                    var roles = getRoles();
                    _context.Roles.AddRange(roles);
                    _context.SaveChanges();
                }
            }
        }

        public static IEnumerable<Role> getRoles()
        {
            return new List<Role>()
            {
                new Role()
                {
                    Name = "Admin"
                },
                new Role()
                {
                    Name = "Moderator"
                },
                new Role()
                {
                    Name = "User"
                }
            };
        }
    }
}
