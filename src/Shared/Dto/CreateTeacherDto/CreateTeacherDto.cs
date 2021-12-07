using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto.CreateTeacherDto
{
    public class CreateTeacherDto : IMap
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int HoursAvailability { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateTeacherDto, Teacher>();
        }
    }
}
