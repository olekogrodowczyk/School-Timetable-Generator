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
    public class AvailabilityVm : IMap
    {
        public int StartsAt { get; set; }
        public int EndsAt { get; set; }
        public string DayOfWeek { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Availability, AvailabilityVm>();
        }
    }
}
