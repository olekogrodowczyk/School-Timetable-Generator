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
    public class SubjectVm : IMap
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<GroupVm> Groups { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Subject, SubjectVm>();                
        }
    }
}
