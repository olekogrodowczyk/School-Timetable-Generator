using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto.CreateTimetableDto
{
    public class CreateTimetableDtoValidator : AbstractValidator<CreateTimetableDto>
    {
        public CreateTimetableDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nazwa planu lekcji nie może być pusta")
                .MinimumLength(5).WithMessage("Minimalna długość nazwy planu lekcji to 5");
        }
    }
}