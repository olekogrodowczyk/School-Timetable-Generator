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
        private readonly IUserRepository _userRepository;

        public CreateClassroomDtoValidator(IClassroomRepository classroomRepository, IUserRepository userRepository)
        {
            _classroomRepository = classroomRepository;
            _userRepository = userRepository;

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Kod klasy nie może być pusty")
                .MaximumLength(5).WithMessage("Maksymalna długość kodu to 5 znaków")
                .MustAsync(ClassroomNotExists).WithMessage("Podana sala już istnieje");

            RuleFor(x => x.NumberOfSeats)
                .NotEmpty().WithMessage("Ilość miejsc nie może być pusta")
                .GreaterThan(0).WithMessage("Minimalna ilość miejsc w sali to 1");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nazwa sali nie może być pusta");
        }

        public async Task<bool> ClassroomNotExists(string value, CancellationToken cancellationToken)
        {
            int activeTimetableId = await _userRepository.GetCurrentActiveTimetable();
            bool classroomExists = await _classroomRepository.AnyAsync(x => x.TimetableId == activeTimetableId && x.Code == value);
            if(classroomExists) { return false; }
            return true;
        }
    }
}