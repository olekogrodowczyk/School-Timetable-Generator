using Application.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GroupService : IGroupService
    {
        private readonly IMapper _mapper;

        public GroupService(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}