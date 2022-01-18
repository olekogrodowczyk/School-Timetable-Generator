using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UnassignedLesson : AuditableEntity
    {
        public int ClassId { get; set; }
        public virtual Class Class { get; set; }
        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
        public int? TimetableId { get; set; }
        public virtual TimeTable TimeTable { get; set; }
    }
}
