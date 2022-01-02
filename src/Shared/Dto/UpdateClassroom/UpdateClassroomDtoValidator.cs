using Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto.UpdateClassroom
{
    public class UpdateClassroomDtoValidator : AbstractValidator<UpdateClassroomDto>
    {
        private readonly IClassroomRepository _classroomRepository;
        private readonly ITimetableRepository _timetableRepository;
        public UpdateClassroomDtoValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Kod klasy nie może być pusty")
                .MaximumLength(5).WithMessage("Maksymalna długość kodu to 5 znaków");

            RuleFor(x => x.NumberOfSeats)
                .NotEmpty().WithMessage("Ilość miejsc nie może być pusta")
                .GreaterThan(0).WithMessage("Minimalna ilość miejsc w sali to 1");

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Identyfikator nie może być pusty!")
                .GreaterThan(0).WithMessage("Błędna wartość identyfikatora");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nazwa sali nie może być pusta");
        }
    }
    }

