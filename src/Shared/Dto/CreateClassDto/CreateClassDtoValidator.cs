using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto.CreateClassDto
{
    public class CreateClassDtoValidator : AbstractValidator<CreateClassDto>
    {
        public CreateClassDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nie podano nazwy klasy")
                .MinimumLength(5).WithMessage("Minimalna długość nazwy grupy to 5");
        }
    }
}
