using Application.Mappings;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ViewModels
{
    public class TimetableOutcomeVm
    {
        public string ClassName { get; set; }
        public IList<LessonVm> Lessons { get; set; } = new List<LessonVm>();
    }
}
