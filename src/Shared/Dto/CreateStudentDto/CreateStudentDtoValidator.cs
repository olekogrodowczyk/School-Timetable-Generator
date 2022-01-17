using Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Dto.CreateStudentDto
{
    public class CreateStudentDtoValidator : AbstractValidator<CreateStudentDto>
    {
        private readonly ITimetableRepository _timetableRepository;
        private readonly IUserRepository _userRepository;
        private readonly IStudentRepository _studentRepository;

        public CreateStudentDtoValidator(ITimetableRepository timetableRepository, IUserRepository userRepository
            , IStudentRepository studentRepository)
        {
            _timetableRepository = timetableRepository;
            _userRepository = userRepository;
            _studentRepository = studentRepository;

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Imię nauczyciela nie może być puste");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Nazwisko nauczyciela nie może być puste");
            RuleFor(x => new { x.FirstName, x.LastName }).MustAsync(async (x, y) => await StudentNotExists(x.FirstName, x.LastName))
                .WithMessage(x=> $"Podany uczeń: {x.FirstName} {x.LastName} już istnieje");
        }

        public async Task<bool> StudentNotExists(string firstName, string lastName)
        {
            int activeTimetableId = await _userRepository.GetCurrentActiveTimetable();
            bool studentExists = await _studentRepository
                .AnyAsync(x => x.FirstName == firstName && x.LastName == lastName && x.TimetableId == activeTimetableId);
            if (studentExists) { return false; }
            return true;
        }
    }
}