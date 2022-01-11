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
    public class LessonVm : IMap
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public string GroupName { get; set; }
        public string Teacher { get; set; }
        public string ClassroomCode { get; set; }
        public int StartsAt { get; set; }
        public int EndsAt { get; set; }
        public int DayOfWeek { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Lesson, LessonVm>()
                .ForMember(x => x.SubjectName, opt => opt.MapFrom(s => s.Subject.Name))
                .ForMember(x => x.GroupName, opt => opt.MapFrom(g => g.Group.Name))
                .ForMember(x => x.Teacher, opt => opt.MapFrom(t => t.Teacher.FirstName + " " + t.Teacher.LastName))
                .ForMember(x => x.ClassroomCode, opt => opt.MapFrom(c => c.Classroom.Code));
        }        
    }
}
