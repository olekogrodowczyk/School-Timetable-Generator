using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AlgorithmService : IAlgorithmService
    {
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IAvailabilityRepository _availabilityRepository;
        private readonly IClassRepository _classRepository;
        private readonly IClassroomRepository _classroomRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly ITimetableRepository _timetableRepository;
        private readonly IUserRepository _userRepository;

        public AlgorithmService(IAssignmentRepository assignmentRepository, IAvailabilityRepository availabilityRepository
            , IClassRepository classRepository, IClassroomRepository classroomRepository, IGroupRepository groupRepository
            , ILessonRepository lessonRepository, IStudentRepository studentRepository, ISubjectRepository subjectRepository
            , ITeacherRepository teacherRepository, ITimetableRepository timetableRepository, IUserRepository userRepository)
        {
            _assignmentRepository = assignmentRepository;
            _availabilityRepository = availabilityRepository;
            _classRepository = classRepository;
            _classroomRepository = classroomRepository;
            _groupRepository = groupRepository;
            _lessonRepository = lessonRepository;
            _studentRepository = studentRepository;
            _subjectRepository = subjectRepository;
            _teacherRepository = teacherRepository;
            _timetableRepository = timetableRepository;
            _userRepository = userRepository;
        }

        public async Task TutorialAsync()
        {
            //Pobierz wszystkich nauczycieli z bazy
            var allTeachers = await _teacherRepository.GetAllAsync();

            //Pobierz wszystkich nauczycieli z bazy o imieniu Marek
            var teachersId2 = await _teacherRepository.GetWhereAsync(x => x.FirstName == "Marek");

            //Pobierz wszystkich studentów z bazy o nazwisku Kowalski z JOINAMI
            //Ilość JOINów można używać ile się chce
            var studentsId2Joins = await _studentRepository.GetWhereAsync(x => x.LastName == "Kowalski", x => x.Assignments, x => x.Class);

            //Pobierz studenta o id=3 z JOINEM
            var student = await _studentRepository.GetByIdAsync(5, x => x.Assignments);

            //Dodaj nowy przedmiot
            //Id doda się samo
            var subject = new Subject { Name = "Matematyka" };
            await _subjectRepository.AddAsync(subject);
            Console.WriteLine(subject.Id); //Wypisze przypisane ID do bazy

            //Zaktualizowanie wcześniej dodanego przedmiotu
            //Najpierw szukamy przedmiotu (jak nie znajdzie, funkcja zwróci null)
            //subject i subjectFromDatabase to tam sama referencja z bazy
            var subjectFromDatabase = await _subjectRepository.SingleOrDefaultAsync(x => x.Name == "Matematyka");
            subjectFromDatabase.Name = "Fizyka";
            await _subjectRepository.UpdateAsync(subject);

            //Sprawdzenie czy istnieje jakikolwiek nauczyciel z podanym imieniem
            const string name = "Maciuś";
            bool exists = await _teacherRepository.AnyAsync(x => x.FirstName == name);
        }

        public async Task Init()
        {
            Console.WriteLine("Hello world");
        }
    }
}