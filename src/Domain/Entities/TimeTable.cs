using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TimeTable : AuditableEntity
    {
        public int CreatorId { get; set; }
        public virtual User Creator { get; set; }
        public string Name { get; set; }
        public int CurrentPhase { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<Classroom> Classrooms { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}