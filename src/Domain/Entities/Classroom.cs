using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Classroom : AuditableEntity
    {
        public int? TimetableId { get; set; }
        public virtual TimeTable TimeTable { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int NumberOfSeats { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
    }
}