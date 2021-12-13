using Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Dto.CreateSubjectDto
{
    public class CreateSubjectDtoValidator : AbstractValidator<CreateSubjectDto>
    {
        private readonly ITimetableRepository _timetableRepository;

        public CreateSubjectDtoValidator(ITimetableRepository timetableRepository)
        {
            _timetableRepository = timetableRepository;

            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("Nazwa przedmiotu nie może być pusta")
                .MinimumLength(1).WithMessage("Minimalna długość przedmiotu to 1");

            RuleFor(s => s.TimetableId)
                .MustAsync(TimetableExists).WithMessage("Podany plan lekcji nie istnieje");
        }

        private async Task<bool> TimetableExists(int value, CancellationToken cancellationToken)
        {
            return await _timetableRepository.AnyAsync(x => x.Id == value);
        }
    }
}