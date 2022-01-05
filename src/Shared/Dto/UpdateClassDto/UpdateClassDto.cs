﻿using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto.UpdateClassDto
{
    public class UpdateClassDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TeacherName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateClassDto, Class>();
        }
    }
}
