using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ViewModels
{
    public class GroupVm : IMap
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfLessonInWeek { get; set; }
        public string Teacher { get; set; }
        public IEnumerable<AssignmentVm> Students { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Group, GroupVm>()
                .ForMember(x => x.Teacher, opt => opt.MapFrom(y => y.Teacher.FirstName + " " + y.Teacher.LastName))
                .ForMember(x => x.Students, opt => opt.MapFrom(y => y.Assignments));
        }
    }
}
