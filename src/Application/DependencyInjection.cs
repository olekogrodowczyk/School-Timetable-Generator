using Application.Interfaces;
using Application.Mappings;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Reflection;
using Domain.Interfaces;
using FluentValidation.AspNetCore;
using Application.Services;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<ITimetableService, TimetableService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<IAvailabilityService, AvailabilityService>();
            services.AddScoped<IClassroomService, ClassroomService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IAlgorithmService, AlgorithmService>();

            return services;
        }
    }
}