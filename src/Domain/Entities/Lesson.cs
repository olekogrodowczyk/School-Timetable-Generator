using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Lesson : AuditableEntity
    {
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
        public int? TimetableId { get; set; }
        public virtual TimeTable TimeTable { get; set; }
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
        public int ClassroomId { get; set; }
        public virtual Classroom Classroom { get; set; }
        public int StartsAt { get; set; }
        public int EndsAt { get; set; }
        [Range(1,5)]
        public int DayOfWeek { get; set; }
    }
}