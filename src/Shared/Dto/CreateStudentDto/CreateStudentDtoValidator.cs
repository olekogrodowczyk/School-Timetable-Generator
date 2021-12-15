using Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Dto.CreateStudentDto
{
    public class CreateStudentDtoValidator : AbstractValidator<CreateStudentDto>
    {
        private readonly ITimetableRepository _timetableRepository;

        public CreateStudentDtoValidator(ITimetableRepository timetableRepository)
        {
            _timetableRepository = timetableRepository;

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