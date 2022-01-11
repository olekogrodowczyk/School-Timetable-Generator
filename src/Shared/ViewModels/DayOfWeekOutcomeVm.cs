using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ViewModels
{
    public class DayOfWeekOutcomeVm
    {
        public int DayOfWeek { get; set; }
        public IList<LessonVm> Lessons { get; set; } = new List<LessonVm>();
    }
}
