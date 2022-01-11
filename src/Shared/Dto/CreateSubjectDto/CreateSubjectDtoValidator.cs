using Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Dto.CreateSubjectDto
{
    public class CreateSubjectDtoValidator : AbstractValidator<CreateSubjectDto>
    {
        private readonly ITimetableRepository _timetableRepository;
        private readonly IClassRepository _classRepository;
        private readonly IUserRepository _userRepository;

        public CreateSubjectDtoValidator(ITimetableRepository timetableRepository, IClassRepository classRepository
            , IUserRepository userRepository)
        {
            _timetableRepository = timetableRepository;
            _classRepository = classRepository;
            _userRepository = userRepository;
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("Nazwa przedmiotu nie może być pusta")
                .MinimumLength(1).WithMessage("Minimalna długość przedmiotu to 1");

            RuleFor(s => s.ClassName)
                .NotEmpty().WithMessage("Nazwa klasy nie może być pusta")
                .MustAsync(ClassExists).WithMessage("Podana klasa nie istnieje");
        }

        private async Task<bool> ClassExists(string value, CancellationToken cancellationToken)
        {
            int activeTimetableId = await _userRepository.GetCurrentActiveTimetable();
            bool exists = await _classRepository.AnyAsync(x => x.TimetableId == activeTimetableId && x.Name == value);
            return exists;
        }
    }
}