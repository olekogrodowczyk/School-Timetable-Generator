using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto.UpdateTeacherDto
{
    public class UpdateTeacherDtoValidator : AbstractValidator<UpdateTeacherDto>
    {
        public UpdateTeacherDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Imię nauczyciela nie może być puste");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Nazwisko nauczyciela nie może być puste");
            RuleFor(x => x.HoursAvailability)
                .GreaterThan(0).WithMessage("Podano nieprawidłową dostępność");
        }
    }
}
