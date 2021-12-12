using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto.CreateClassroomDto
{
    public class CreateClassroomDtoValidator : AbstractValidator<CreateClassroomDto>
    {
        public CreateClassroomDtoValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Kod klasy nie może być pusty")
                .MaximumLength(5).WithMessage("Maksymalna długość kodu to 5 znaków");

            RuleFor(x => x.NumberOfSeats)
                .NotEmpty().WithMessage("Ilość miejsc nie może być pusta")
                .GreaterThan(0).WithMessage("Minimalna ilość miejsc w sali to 1");
        }
    }
}
