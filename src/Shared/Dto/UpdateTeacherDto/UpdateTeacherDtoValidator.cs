using Domain.Interfaces;
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
        private readonly IUserRepository _userRepository;
        private readonly ITeacherRepository _teacherRepository;

        public UpdateTeacherDtoValidator(IUserRepository userRepository, ITeacherRepository teacherRepository)
        {
            _userRepository = userRepository;
            _teacherRepository = teacherRepository;

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Imię nauczyciela nie może być puste");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Nazwisko nauczyciela nie może być puste");
            RuleFor(x => x.HoursAvailability)
                .GreaterThan(0).WithMessage("Podano nieprawidłową dostępność");
            RuleFor(x => new { x.FirstName, x.LastName }).MustAsync(async (x, y) => await TeacherNotExists(x.FirstName, x.LastName))
                .WithMessage("Podany nauczyciel już istnieje");
        }

        public async Task<bool> TeacherNotExists(string firstName, string lastName)
        {
            int activeTimetableId = await _userRepository.GetCurrentActiveTimetable();
            int teachers = await _teacherRepository
                .GetCount(x => x.FirstName == firstName && x.LastName == lastName && x.TimetableId == activeTimetableId);
            if (teachers>2) { return false; }
            return true;
        }
    }
}
