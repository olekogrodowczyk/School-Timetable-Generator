using Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Dto.CreateGroupDto
{
    public class CreateGroupDtoValidator : AbstractValidator<CreateGroupDto>
    {
        private readonly IClassRepository _classRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITimetableRepository _timetableRepository;
        private readonly IUserRepository _userRepository;

        public CreateGroupDtoValidator(IClassRepository classRepository, IStudentRepository studentRepository
            , ITeacherRepository teacherRepository, ISubjectRepository subjectRepository, ITimetableRepository timetableRepository
            ,IUserRepository userRepository)
        {
            _classRepository = classRepository;
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
            _subjectRepository = subjectRepository;
            _timetableRepository = timetableRepository;
            _userRepository = userRepository;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nazwa grupy nie może być pusta")
                .MinimumLength(2).WithMessage("Minimalna długość nazwy grupy to 2");

            RuleFor(x => x.StudentIds)
                .NotEmpty().WithMessage("Grupa nie może być pusta")
                .MustAsync(StudentsExist).WithMessage("Podano nie istniejącego ucznia");

            RuleFor(x => x.ClassName)
                .NotEmpty().WithMessage("Nazwa klasy nie może być pusta")
                .MustAsync(ClassExists).WithMessage("Podana klasa nie istnieje");

            RuleFor(x => x.TeacherName)
                .NotEmpty().WithMessage("Nie podano wychowawcy")
                .MustAsync(TeacherExists).WithMessage("Podany nauczyciel nie istnieje");

            RuleFor(x => x.SubjectName)
                .MustAsync(SubjectExists)
                .When(x => !string.IsNullOrEmpty(x.SubjectName))
                .WithMessage("Podany przedmiot nie istnieje");

            RuleFor(x => x.NumberOfLessonsInWeek)
                .GreaterThan(0).WithMessage("Ilość dostępnych godzin musi być większa od 0");
        }

        public async Task<bool> ClassExists(string value, CancellationToken cancellationToken)
        {
            return await _classRepository.AnyAsync(c => c.Name == value);
        }

        public async Task<bool> StudentsExist(IEnumerable<int> values, CancellationToken cancellationToken)
        {
            if (values is null) { return false; }
            foreach (int id in values)
            {
                if (!await _studentRepository.AnyAsync(s => s.Id == id)) { return false; }
            }
            return true;
        }

        public async Task<bool> TeacherExists(string value, CancellationToken cancellationToken)
        {
            int activeTimetableId = await _userRepository.GetCurrentActiveTimetable();
            return await _teacherRepository.AnyAsync(t => t.TimetableId==activeTimetableId && t.FirstName + " " + t.LastName == value);
        }

        public async Task<bool> SubjectExists(string value, CancellationToken cancellationToken)
        {
            return await _subjectRepository.AnyAsync(s => s.Name == value);
        }
    }
}