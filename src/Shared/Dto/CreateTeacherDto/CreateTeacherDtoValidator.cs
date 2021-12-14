using Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Dto.CreateTeacherDto
{
    public class CreateTeacherDtoValidator : AbstractValidator<CreateTeacherDto>
    {
        private readonly ITimetableRepository _timetableRepository;

        public CreateTeacherDtoValidator(ITimetableRepository timetableRepository)
        {
            _timetableRepository = timetableRepository;

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Imię nauczyciela nie może być puste");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Nazwisko nauczyciela nie może być puste");
            RuleFor(x => x.HoursAvailability)
                .GreaterThan(0).WithMessage("Podano nieprawidłową dostępność");
            RuleFor(x => x.TimetableId)
                .NotEmpty().WithMessage("Nie podano planu lekcji")
                .MustAsync(TimetableExists).WithMessage("Podany plan lekcji nie istnieje");
        }

        private async Task<bool> TimetableExists(int value, CancellationToken cancellationToken)
        {
            return await _timetableRepository.AnyAsync(x => x.Id == value);
        }
    }
}