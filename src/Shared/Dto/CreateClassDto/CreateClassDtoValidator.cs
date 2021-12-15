using Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Dto.CreateClassDto
{
    public class CreateClassDtoValidator : AbstractValidator<CreateClassDto>
    {
        private readonly ITimetableRepository _timetableRepository;
        private readonly IClassRepository _classRepository;
        private readonly ITeacherRepository _teacherRepository;

        public CreateClassDtoValidator(ITimetableRepository timetableRepository, IClassRepository classRepository
            , ITeacherRepository teacherRepository)
        {
            _timetableRepository = timetableRepository;
            _classRepository = classRepository;
            _teacherRepository = teacherRepository;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nie podano nazwy klasy")
                .MinimumLength(1).WithMessage("Minimalna długość nazwy klasy to 1");

            RuleFor(x => x.TeacherName)
                .NotEmpty().WithMessage("Nie podano wychowawcy")
                .MustAsync(teacherExists).WithMessage("Podany nauczyciel nie istnieje");
        }

        private async Task<bool> teacherExists(string value, CancellationToken cancellationToken)
        {
            return await _teacherRepository.AnyAsync(t => t.FirstName + " " + t.LastName == value);
        }
    }
}