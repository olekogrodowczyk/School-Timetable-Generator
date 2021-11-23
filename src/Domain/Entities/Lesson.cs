using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Lesson : AuditableEntity
    {
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }
    }
}
