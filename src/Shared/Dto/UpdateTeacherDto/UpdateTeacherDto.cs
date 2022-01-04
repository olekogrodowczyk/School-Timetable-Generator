using Application.Mappings;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dto.CreateAvailabilityDto;
using Shared.ViewModels;
using Domain.Entities;

namespace Shared.Dto.UpdateTeacherDto
{
    public class UpdateTeacherDto : IMap
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int HoursAvailability { get; set; }
        public IEnumerable<CreateAvailabilityDto.CreateAvailabilityDto> Availabilities { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateTeacherDto, Teacher>()
                .ForMember(x => x.Availabilities, opt => opt.Ignore());
        }
    }
}
