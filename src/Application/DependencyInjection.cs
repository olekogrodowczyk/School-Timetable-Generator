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

            return services;
        }
    }
}
