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

        static int deep = 0;
        static List<int> periods = new List<int>() { 0, 1, 2, 3 };
        static List<Group> lessonss = new List<Group>();
        static List<Classroom> classess = new List<Classroom>();
        static int inMemory = 0;
        static int licznik = 0;
        static List<List<Group>> timeLessonss = new List<List<Group>>();
        static List<List<Group>> timeLessonssCorrect = new List<List<Group>>();
        static List<Group> errorList = new List<Group>();
        static int day_periods = 40;

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


        static private bool PlaceLesson(Group lesson, int nothere = 9999)
        {
            List<int> howMany = new List<int>();

            for (int i = 0; i < periods.Count; i++)
            {
                if ((i == nothere) && (CountErrors(i, lesson)) > 0)
                    howMany.Add(999);

                else
                  if ((classMaker(i, lesson.ClassId /* tu ma byc ilosc osob a nie classId */)) == -1)
                {
                    howMany.Add(CountErrors(i, lesson) + 1);
                }
                else
                {
                    howMany.Add(CountErrors(i, lesson));
                }
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
                lesson.ClassroomId = classMaker(finalList[final], lesson.ClassId);
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
                if (lesson.ClassId == null)
                {
                    int a = classMaker(finalList[final], lesson.ClassId);
                    lesson.ClassId = a;
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
                    PlaceLesson(inMemory[i], finalList[final]);
                    deep--;
                }

            }

            return true;
        }


        static private int CountErrors(int period, Group lesson)
        {
            int count = 0;

            for (int i = 0; i < timeLessonss[period].Count(); i++)
            {
                if ((timeLessonss[period][i].ClassId == lesson.ClassId) || (timeLessonss[period][i].TeacherId == lesson.TeacherId))
                    count++;

            }

            return count;

        }

        static private int classMaker(int period, int lessonPeople)
        {
            return 0;
        }

        // prace w toku
        /*
        static private int classMaker(int period, int lessonPeople)
        {

            List<int> notAvailable = new List<int>();
            List<int> available = new List<int>();
            int a = 0;

            for (int i = 0; i < timeLessonss[period].Count(); i++)
            {
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
                    available.Add(classess[i].Id-1);
            }

            if (available.Count == 0)
                return -1;
            int min = 999;
            int indeks = 1;
            for (int i = 0; i < available.Count; i++)
            {

                a = lessonss[available[i]].ClassId;
                if ((a > lessonPeople) && (a < min))
                {
                    min = a;
                    indeks = i;

                }

            }
            if (min == 0)
                return -1;
            else
                return indeks;
        }


        */

        public async Task Init()
        {

            var Classess = await _classroomRepository.GetAllAsync();
            var Lessonss = await _groupRepository.GetAllAsync();
            lessonss = Lessonss.ToList();
            classess = Classess.ToList();


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
                PlaceLesson(lessonss[i]);
                if (inMemory + 1 < errorList.Count())
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
                    Console.WriteLine("Nauczyciel: " + timeLessonss[x][y].TeacherId + " Klasa: " + timeLessonss[x][y].ClassId + " Start o godzinie: " + hourmakerStart(x).ToString() + " Koniec o godzinie: " +
                        hourmakerEnd(x) + " Sala: " + timeLessonss[x][y].ClassroomId);


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
        }


        static private int dayMaker(int period)
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

        static private int hourmakerStart(int period)
        {
            return (period % day_periods) + 8;
        }
        static private int hourmakerEnd(int period)
        {
            return (period % day_periods) + 9;
        }










    }


}