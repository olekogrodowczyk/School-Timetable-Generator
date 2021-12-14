using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Subject : AuditableEntity
    {
        public string Name { get; set; }
        public int? TimetableId { get; set; }
        public virtual TimeTable TimeTable { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
    }
}