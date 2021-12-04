using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto.CreateTimetableDto
{
    public class CreateTimetableDto : IMap
    {
        public string Name { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateTimetableDto, TimeTable>().ReverseMap();
        }
    }
}
