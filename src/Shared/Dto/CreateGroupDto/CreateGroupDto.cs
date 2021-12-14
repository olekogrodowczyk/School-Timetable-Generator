using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto.CreateGroupDto
{
    public class CreateGroupDto
    {
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string SubjectName { get; set; }
        public string TeacherName { get; set; }
        public int TimetableId { get; set; }
        public int NumberOfLessonsInWeek { get; set; }
        public IEnumerable<int> StudentIds { get; set; }
    }
}