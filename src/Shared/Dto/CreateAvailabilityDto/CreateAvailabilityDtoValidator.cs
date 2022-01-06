using Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Dto.CreateAvailabilityDto
{
    public class CreateAvailabilityDtoValidator : AbstractValidator<CreateAvailabilityDto>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly ITimetableRepository _timetableRepository;
        private readonly IUserRepository _userRepository;
        private string[] daysOfWeek = { "Poniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek", "Sobota", "Niedziela" };

        public CreateAvailabilityDtoValidator(ITeacherRepository teacherRepository, ITimetableRepository timetableRepository
            , IUserRepository userRepository)
        {
            _teacherRepository = teacherRepository;
            _timetableRepository = timetableRepository;
            _userRepository = userRepository;

            RuleFor(x => x.DayOfWeek)
                .NotEmpty().WithMessage("Podany dzień tygodnia jest pusty")
                .MustAsync(DefinedDayOfWeekValid).WithMessage("Podany dzień tygodnia jest nieprawidłowy");

            RuleFor(x => x.StartsAt)
                .InclusiveBetween(8, 16).WithMessage("Podano nieprawidłową czas startu");

            RuleFor(x => x.EndsAt)
                .InclusiveBetween(9, 17).WithMessage("Podano nie prawidłowy czas zakończenia");

            RuleFor(x => x.TeacherId)
                .MustAsync(TeacherExists).WithMessage("Podany nauczyciel nie istnieje");
        }

        public Task<bool> DefinedDayOfWeekValid(string value, CancellationToken cancellationToken)
        {
            return Task.FromResult(daysOfWeek.Contains(value));
        }

        public async Task<bool> TeacherExists(int value, CancellationToken cancellationToken)
        {
            int activeTimetableId = await _userRepository.GetCurrentActiveTimetable();
            return await _teacherRepository.AnyAsync(t => t.TimetableId==activeTimetableId && t.Id==value);
        }
    }
}