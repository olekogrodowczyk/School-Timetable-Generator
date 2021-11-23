using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Group : AuditableEntity
    {
        public int ClassId { get; set; }
        public virtual Class Class { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
