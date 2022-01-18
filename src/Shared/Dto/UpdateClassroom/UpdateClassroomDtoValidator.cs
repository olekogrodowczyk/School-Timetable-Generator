using Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Dto.UpdateClassroom
{
    public class UpdateClassroomDtoValidator : AbstractValidator<UpdateClassroomDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IClassroomRepository _classroomRepository;

        public UpdateClassroomDtoValidator(IUserRepository userRepository, IClassroomRepository classroomRepository)
        {
            _userRepository = userRepository;
            _classroomRepository = classroomRepository;

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Kod klasy nie może być pusty")
                .MaximumLength(5).WithMessage("Maksymalna długość kodu to 5 znaków")
                .MustAsync(ClassroomNotExists).WithMessage("Podana sala już istnieje");

            RuleFor(x => x.NumberOfSeats)
                .NotEmpty().WithMessage("Ilość miejsc nie może być pusta")
                .GreaterThan(0).WithMessage("Minimalna ilość miejsc w sali to 1");

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Identyfikator nie może być pusty!")
                .GreaterThan(0).WithMessage("Błędna wartość identyfikatora");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nazwa sali nie może być pusta");            
        }

        public async Task<bool> ClassroomNotExists(string value, CancellationToken cancellationToken)
        {
            int activeTimetableId = await _userRepository.GetCurrentActiveTimetable();
            int classrooms = await _classroomRepository.GetCount(x => x.TimetableId == activeTimetableId && x.Code == value);
            if (classrooms>1) { return false; }
            return true;
        }
    }
    }

