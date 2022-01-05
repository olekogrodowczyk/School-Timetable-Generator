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
    public class ClassVm : IMap
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TimetableId { get; set; }
        public string Teacher { get; set; }
        public IEnumerable<StudentVm> Students { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Class, ClassVm>()
                .ForMember(x => x.Teacher, opt => opt.MapFrom(y => y.Teacher.FirstName + " " + y.Teacher.LastName));
        }
    }
}