using Application.Interfaces;
using Domain.Interfaces;
using Shared.Dto.CreateClassDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;
        private readonly IStudentRepository _studentRepository;

        public ClassService(IClassRepository classRepository, IStudentRepository studentRepository)
        {
            _classRepository = classRepository;
            _studentRepository = studentRepository;
        }

        public async Task CreateClass(CreateClassDto model)
        {

        }
    }
}
