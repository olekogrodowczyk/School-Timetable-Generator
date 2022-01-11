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
    public class TimetableVm : IMap
    {
        public int Id { get; set; }
        public DateTime GenereteTime { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TimeTable, TimetableVm>();
        }
    }
}
