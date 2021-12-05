using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Availability : AuditableEntity
    {
        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
        public int StartsAt { get; set; }
        public int EndsAt { get; set; }
        public string DayOfWeek { get; set; }
    }
}
