﻿using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto.UpdateClassroom
{
    public class UpdateClassroomDto : IMap
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int NumberOfSeats { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateClassroomDto, Classroom>();
        }
    }
}
