﻿using Shared.Dto.CreateTeacherDto;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Services.Models;

namespace UI.Services.Interfaces
{
    public interface ITeacherHttpService
    {
        Task CreateTeachersWithStudents(List<TeacherModel> models);

        Task<IEnumerable<TeacherVm>> GetAllTeachersFromTimetable(int timetableId);
    }
}