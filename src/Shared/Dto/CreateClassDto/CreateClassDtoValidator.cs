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
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUserRepository _userRepository;

        public CreateClassDtoValidator(ITimetableRepository timetableRepository, IClassRepository classRepository
            , ITeacherRepository teacherRepository, IUserRepository userRepository)
        {
            _timetableRepository = timetableRepository;
            _classRepository = classRepository;
            _teacherRepository = teacherRepository;
            _userRepository = userRepository;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nie podano nazwy klasy")
                .MinimumLength(1).WithMessage("Minimalna długość nazwy klasy to 1");

            RuleFor(x => x.TeacherName)
                .NotEmpty().WithMessage("Nie podano wychowawcy")
                .MustAsync(TeacherExists).WithMessage("Podany nauczyciel nie istnieje");

        }

        public async Task<bool> TeacherExists(string value, CancellationToken cancellationToken)
        {
            int activeTimetableId = await _userRepository.GetCurrentActiveTimetable();
            return await _teacherRepository.AnyAsync(t => t.TimetableId==activeTimetableId && t.FirstName + " " + t.LastName == value);
        }
    }
}