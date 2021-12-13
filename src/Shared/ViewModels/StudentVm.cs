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
    public class StudentVm : IMap
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Class { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Student, StudentVm>()
                .ForMember(x => x.Class, opt => opt.MapFrom(y => y.Class.Name));
        }
    }
}