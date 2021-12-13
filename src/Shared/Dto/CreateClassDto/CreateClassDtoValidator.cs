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

        public CreateClassDtoValidator(ITimetableRepository timetableRepository, IClassRepository classRepository)
        {
            _timetableRepository = timetableRepository;
            _classRepository = classRepository;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nie podano nazwy klasy")
                .MinimumLength(1).WithMessage("Minimalna długość nazwy klasy to 1");

            RuleFor(x => x.TimetableId)
                .GreaterThan(0).WithMessage("Podano nie poprawny plan lekcji")
                .MustAsync(TimetableExists).WithMessage("Podany plan lekcji nie istnieje");
        }

        private async Task<bool> TimetableExists(int value, CancellationToken cancellationToken)
        {
            return await _timetableRepository.AnyAsync(x => x.Id == value);
        }
    }
}