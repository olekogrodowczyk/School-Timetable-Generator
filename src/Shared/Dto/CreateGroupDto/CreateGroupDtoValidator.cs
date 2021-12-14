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

        public CreateGroupDtoValidator(IClassRepository classRepository, IStudentRepository studentRepository
            , ITeacherRepository teacherRepository, ISubjectRepository subjectRepository)
        {
            _classRepository = classRepository;
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
            _subjectRepository = subjectRepository;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nazwa grupy nie może być pusta")
                .MinimumLength(2).WithMessage("Minimalna długość nazwy grupy to 2");

            RuleFor(x => x.StudentIds)
                .NotEmpty().WithMessage("Grupa nie może być pusta")
                .MustAsync(studentsExist).WithMessage("Podano nie istniejącego ucznia");

            RuleFor(x => x.ClassName)
                .NotEmpty().WithMessage("Nazwa klasy nie może być pusta")
                .MustAsync(classExists).WithMessage("Podana klasa nie istnieje");

            RuleFor(x => x.TeacherName)
                .NotEmpty().WithMessage("Nie podano wychowawcy")
                .MustAsync(teacherExists).WithMessage("Podany nauczyciel nie istnieje");

            RuleFor(x => x.SubjectName)
                .MustAsync(subjectExists)
                .When(x => !string.IsNullOrEmpty(x.SubjectName))
                .WithMessage("Podany przedmiot nie istnieje");

            RuleFor(x => x.NumberOfLessonsInWeek)
                .GreaterThan(0).WithMessage("Ilość dostępnych godzin musi być większa od 0");
        }

        private async Task<bool> classExists(string value, CancellationToken cancellationToken)
        {
            return await _classRepository.AnyAsync(c => c.Name == value);
        }

        private async Task<bool> studentsExist(IEnumerable<int> values, CancellationToken cancellationToken)
        {
            foreach (int id in values)
            {
                if (!await _studentRepository.AnyAsync(s => s.Id == id)) { return false; }
            }
            return true;
        }

        private async Task<bool> teacherExists(string value, CancellationToken cancellationToken)
        {
            return await _teacherRepository.AnyAsync(t => t.FirstName + " " + t.LastName == value);
        }

        private async Task<bool> subjectExists(string value, CancellationToken cancellationToken)
        {
            return await _subjectRepository.AnyAsync(s => s.Name == value);
        }
    }
}