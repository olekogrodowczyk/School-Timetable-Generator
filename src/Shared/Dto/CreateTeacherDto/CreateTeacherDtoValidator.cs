using Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Dto.CreateTeacherDto
{
    public class CreateTeacherDtoValidator : AbstractValidator<CreateTeacherDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITeacherRepository _teacherRepository;

        public CreateTeacherDtoValidator(IUserRepository userRepository, ITeacherRepository teacherRepository)
        {
            _userRepository = userRepository;
            _teacherRepository = teacherRepository;
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Imię nauczyciela nie może być puste");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Nazwisko nauczyciela nie może być puste");
            RuleFor(x => x.HoursAvailability)
                .GreaterThan(0).WithMessage("Podano nieprawidłową dostępność");
            RuleFor(x => new { x.FirstName, x.LastName }).MustAsync(async (x, y) => await TeacherExists(x.FirstName, x.LastName))
                .WithMessage("Podany nauczyciel już istnieje");
        }

        private async Task<bool> TeacherExists(string firstName, string lastName)
        {
            int activeTimetableId = await _userRepository.GetCurrentActiveTimetable();
            var teacher = await _teacherRepository
                .SingleOrDefaultAsync(x => x.FirstName == firstName && x.LastName == lastName && x.TimetableId == activeTimetableId);
            if(teacher == null) { return true; }
            return false;
        }

        
    }
}