using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto.CreateAvailabilityDto
{
    public class CreateAvailabilityDto : IMap
    {
        public string DayOfWeek { get; set; }
        public int StartsAt { get; set; }
        public int EndsAt { get; set; }
        public int TeacherId { get; set; }
        public int TimetableId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateAvailabilityDto, Availability>();
        }
    }
}