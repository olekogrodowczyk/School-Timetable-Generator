using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Dto.UpdateClassDto
{
    public class UpdateClassDtoValidator : AbstractValidator<UpdateClassDto>
    {
        private readonly ITimetableRepository _timetableRepository;
        private readonly IClassRepository _classRepository;
        private readonly ITeacherRepository _teacherRepository;

        public UpdateClassDtoValidator(ITimetableRepository timetableRepository, IClassRepository classRepository
            , ITeacherRepository teacherRepository)
        {
            _timetableRepository = timetableRepository;
            _classRepository = classRepository;
            _teacherRepository = teacherRepository;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Nie podano identyfikatora");

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
