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
    public class AssignmentVm : IMap
    {
        public int Id { get; set; }
        public string Student { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Assignment, AssignmentVm>()
                .ForMember(x => x.Student, opt => opt.MapFrom(y => y.Student.FirstName + " " + y.Student.LastName));
        }
    }
}
