﻿using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Classroom : AuditableEntity
    {
        public string Code { get; set; }
        public int NumberOfSeats { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
