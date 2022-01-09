using Application.Exceptions;
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
        private readonly ITimetableService _timetableService;

        static int deep = 0;
        static List<int> periods = new List<int>();
        static List<Group> lessonss = new List<Group>();
        static List<Classroom> classess = new List<Classroom>();
        static int inMemory = 0;
        static int licznik = 0;
        static List<List<Group>> timeLessonss = new List<List<Group>>();
        static List<List<Group>> timeLessonssCorrect = new List<List<Group>>();
        static List<Group> errorList = new List<Group>();
        static int day_periods = 8;
        int activeTimetableId;

        public AlgorithmService(IAssignmentRepository assignmentRepository, IAvailabilityRepository availabilityRepository
            , IClassRepository classRepository, IClassroomRepository classroomRepository, IGroupRepository groupRepository
            , ILessonRepository lessonRepository, IStudentRepository studentRepository, ISubjectRepository subjectRepository
            , ITeacherRepository teacherRepository, ITimetableRepository timetableRepository, IUserRepository userRepository
            , ITimetableService timetableService)
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
            _timetableService = timetableService;
        }

        private async Task<bool> PlaceLesson(Group lesson, int nothere = 9999)
        {
            List<int> howMany = new List<int>();

            int counting = 0;
            for (int i = 0; i < periods.Count; i++)
            {
                if ((i == nothere) && (await CountErrors(i, lesson)) > 0)
                    howMany.Add(999);

                else
                  if (await classMaker(i, await counter(lesson)) == -1)
                {
                    counting++;
                    howMany.Add(await CountErrors(i, lesson)+9);
                }
                else
                {
                    howMany.Add(await CountErrors(i, lesson));
                }
            }
            if (counting == periods.Count())
            {
                errorList.Add(lesson);
                return false;
            }
            int minValue = howMany.Min();
            List<int> finalList = new List<int>();
            for (int i = 0; i < periods.Count; i++)
            {
                if (howMany[i] == minValue)
                    finalList.Add(i);
            }
            var rand = new Random();
            int final = rand.Next(finalList.Count() - 1);
            if (minValue == 0)
            {
                lesson.ClassroomId = await classMaker(finalList[final], await counter(lesson));
                timeLessonss[finalList[final]].Add(lesson);
                return true;
            }
            else
            {
                List<Group> inMemory = new List<Group>();
                for (int i = 0; i < timeLessonss[finalList[final]].Count; i++)
                {
                    inMemory.Add(timeLessonss[finalList[final]][i]);
                }
                timeLessonss[finalList[final]].Clear();
                timeLessonss[finalList[final]].Add(lesson);
                if (lesson.ClassroomId == null)
                {
                    int a = await classMaker (finalList[final], await counter(lesson));
                    lesson.ClassroomId = a;
                }
                for (int i = 0; i < inMemory.Count; i++)
                {
                    deep++;
                    if (deep > 10)
                    {
                        for (int j = 0; j < inMemory.Count; j++)
                        {
                            errorList.Add(inMemory[j]);
                        }
                        return false;
                    }
                    licznik++;
                    await PlaceLesson(inMemory[i], finalList[final]);
                    deep--;
                }

            }

            return true;
        }


        private async Task<int> CountErrors(int period, Group lesson)
        {
            int count = 0;

            for (int i = 0; i < timeLessonss[period].Count(); i++)
            {
                if ((timeLessonss[period][i].ClassId == lesson.ClassId) || (timeLessonss[period][i].TeacherId == lesson.TeacherId))
                    count++;

            }

            return count;

        }

        private async Task<int> classMaker(int period, int lessonPeople)
        {

            List<int> notAvailable = new List<int>();
            List<int> available = new List<int>();
            int a = 0;
            

            for (int i = 0; i < timeLessonss[period].Count(); i++)
            {
                if (timeLessonss[period][i].ClassroomId != null)
                    notAvailable.Add((int)timeLessonss[period][i].ClassroomId);
            }
            for (int i = 0; i < classess.Count; i++)
            {
                a = 0;
                for (int j = 0; j < notAvailable.Count; j++)
                {
                    if (notAvailable[j] == classess[i].Id)
                    {
                        a = 1;
                        break;
                    }
                }
                if (a == 0)
                    available.Add(classess[i].Id);
            }

            if (available.Count == 0)
                return -1;
            int min = 999;
            int indeks = 1;
            for (int i = 0; i < available.Count; i++)
            {
                var classroomById = classess.SingleOrDefault(x => x.Id == available[i]);
                a = classroomById.NumberOfSeats;
                if ((a >= lessonPeople) && (a < min))
                {
                    min = a;
                    indeks = available[i];

                }

            }
            if (min == 999)
                return -1;
            else
                return indeks;
        }

        public async Task Init()
        {
            //Exception example
           // throw new AlgorithmException("Wystąpiły błędy podczas generowania planu lekcji, zbyt mała ilość sal " +
        //        ",a grup jest zbyt dużo");

            activeTimetableId = await _userRepository.GetCurrentActiveTimetable();

            for (int i=0;i<40;i++)
            {
                periods.Add(i);
            }

           

            var Classess = await _classroomRepository.GetWhereAsync(c=>c.TimetableId == activeTimetableId);
            var Lessonss = await _groupRepository.GetWhereAsync(g => g.TimetableId == activeTimetableId);
            lessonss = Lessonss.ToList();
            classess = Classess.ToList();

            for(int i=0;i<lessonss.Count;i++)
            {
                lessonss[i].ClassroomId = null;
            }


            for (int i = 0; i < periods.Count; i++)
            {
                timeLessonss.Add(new List<Group>());
                timeLessonssCorrect.Add(new List<Group>());
            }


            for (int i = 0; i < lessonss.Count; i++)
            {
                deep = 0;
                var lista2 = timeLessonss.Select(lst => lst.ToList()).ToList();
                inMemory = errorList.Count();
                await PlaceLesson(lessonss[i]);
                if (inMemory  < errorList.Count())
                {

                    for (int x = 0; x < lista2.Count(); x++)
                    {
                        timeLessonss[x].Clear();
                        for (int y = 0; y < lista2[x].Count(); y++)
                        {

                            timeLessonss[x].Add(lista2[x][y]);

                        }
                    }
                }
            }

            Console.WriteLine("Podłączone zostały nastepujace lekcje: ");
            for (int x = 0; x < timeLessonss.Count; x++)
            {
                for (int y = 0; y < timeLessonss[x].Count; y++)
                {
                    Console.WriteLine("Nauczyciel: " + timeLessonss[x][y].TeacherId + " Klasa: " + timeLessonss[x][y].ClassId + " Start o godzinie: " + (await hourmakerStart(x)).ToString() + " Koniec o godzinie: " +
                        await hourmakerEnd(x) + " Dnia " + await dayMaker(x) + " Sala: " + timeLessonss[x][y].ClassroomId);

                    Lesson toBase = new Lesson();
                    toBase.SubjectId = (int)timeLessonss[x][y].SubjectId;
                    toBase.TeacherId = timeLessonss[x][y].TeacherId;
                    toBase.GroupId = timeLessonss[x][y].Id;
                    toBase.ClassroomId = (int)timeLessonss[x][y].ClassroomId;
                    toBase.StartsAt = await hourmakerStart(x);
                    toBase.EndsAt = await hourmakerEnd (x);
                    toBase.DayOfWeek = await dayMaker (x);
                    await _lessonRepository.AddAsync(toBase);
                }

            }
            Console.WriteLine();
            Console.WriteLine("Nie udalo sie podlaczyc: ");
            Console.WriteLine();

            for (int i = 0; i < errorList.Count; i++)
            {

                Console.WriteLine("Nauczyciel: " + errorList[i].TeacherId + " Klasa: " + errorList[i].ClassId);
            }

            Console.WriteLine("Ciekawostka zostalo uzyte :" + licznik + " rekurencji");
            periods.Clear();
            timeLessonss.Clear();
            timeLessonssCorrect.Clear();
            errorList.Clear();

            
        }

        private async Task handleChangingPhase()
        {            
            const int thirdPhaseNumber = 3;
            await _timetableService.ChangePhaseNumber(activeTimetableId, thirdPhaseNumber);
            await _timetableService.CreateTimetable();
        }

        private async Task<int> dayMaker(int period)
        {
            if (period < day_periods)
                return 1;
            if (period < 2 * day_periods)
                return 2;
            if (period < 3 * day_periods)
                return 3;
            if (period < 4 * day_periods)
                return 4;
            if (period < 5 * day_periods)
                return 5;
            return -1;
        }

        private async Task<int> hourmakerStart(int period)
        {
            return (period % day_periods) + 8;
        }
        private async Task<int> hourmakerEnd(int period)
        {
            return (period % day_periods) + 9;
        }

        private async Task<int> counter(Group x)
        {
            int assignments = await _assignmentRepository.GetCount(y => y.GroupId == x.Id && y.TimetableId == activeTimetableId);
            return assignments;


        }
    }
}