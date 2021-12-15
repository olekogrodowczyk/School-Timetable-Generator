using Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Dto.CreateClassroomDto
{
    public class CreateClassroomDtoValidator : AbstractValidator<CreateClassroomDto>
    {
        private readonly IClassroomRepository _classroomRepository;
        private readonly ITimetableRepository _timetableRepository;

        public CreateClassroomDtoValidator(IClassroomRepository classroomRepository, ITimetableRepository timetableRepository)
        {
            _classroomRepository = classroomRepository;
            _timetableRepository = timetableRepository;
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Kod klasy nie może być pusty")
                .MaximumLength(5).WithMessage("Maksymalna długość kodu to 5 znaków");

            RuleFor(x => x.NumberOfSeats)
                .NotEmpty().WithMessage("Ilość miejsc nie może być pusta")
                .GreaterThan(0).WithMessage("Minimalna ilość miejsc w sali to 1");

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