using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto.CreateStudentDto
{
    public class CreateStudentDto : IMap
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ClassId { get; set; }
        public int TimetableId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateStudentDto, Student>();
        }
    }
}