using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto.CreateClassroomDto
{
    public class CreateClassroomDto : IMap
    {
        public string Code { get; set; }
        public int NumberOfSeats { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateClassroomDto, Classroom>();
        }
    }
}
