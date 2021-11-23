using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Assignment : AuditableEntity
    {
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
    }
}
