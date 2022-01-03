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
    public class TeacherVm : IMap
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<AvailabilityVm> Availabilities { get; set; }
        public int HoursAvailability { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Teacher, TeacherVm>();
        }
    }
}