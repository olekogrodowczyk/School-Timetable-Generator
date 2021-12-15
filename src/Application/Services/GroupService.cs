using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Shared.Dto.CreateGroupDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GroupService : IGroupService
    {
        private readonly IMapper _mapper;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IClassRepository _classRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;

        public GroupService(IMapper mapper, ISubjectRepository subjectRepository, ITeacherRepository teacherRepository
            , IClassRepository classRepository, IStudentRepository studentRepository, IAssignmentRepository assignmentRepository
            , IGroupRepository groupRepository, IUserRepository userRepository)
        {
            _mapper = mapper;
            _subjectRepository = subjectRepository;
            _teacherRepository = teacherRepository;
            _classRepository = classRepository;
            _studentRepository = studentRepository;
            _assignmentRepository = assignmentRepository;
            _groupRepository = groupRepository;
            _userRepository = userRepository;
        }

        public async Task<int> CreateGroup(CreateGroupDto model)
        {
            int activeTimetableId = await _userRepository.GetCurrentActiveTimetable();
            var names = model.TeacherName.Split(" ");
            var subject = await _subjectRepository.SingleOrDefaultAsync(s => s.Name == model.SubjectName);
            var teacher = await _teacherRepository.SingleOrDefaultAsync(t => t.FirstName == names[0] && t.LastName == names[1] && t.TimetableId == activeTimetableId);
            var classEntity = await _classRepository.SingleOrDefaultAsync(c => c.Name == model.ClassName);
            var students = await getStudentEntities(model.StudentIds);

            var group = new Group
            {
                SubjectId = subject.Id,
                TeacherId = teacher.Id,
                ClassId = classEntity.Id,
                Name = model.Name,
                NumberOfLessonInWeek = model.NumberOfLessonsInWeek
            };
            await _groupRepository.AddAsync(group);
            await HandleAssignmentsCreating(students, group.Id);

            return group.Id;
        }

        private async Task HandleAssignmentsCreating(IEnumerable<Student> students, int groupId)
        {
            foreach (Student student in students)
            {
                Assignment assignment = new Assignment
                {
                    StudentId = student.Id,
                    GroupId = groupId
                };
                await _assignmentRepository.AddAsync(assignment);
            }
        }

        private async Task<IEnumerable<Student>> getStudentEntities(IEnumerable<int> StudentIds)
        {
            List<Student> students = new List<Student>();
            foreach (int studentId in StudentIds)
            {
                var student = await _studentRepository.GetByIdAsync(studentId);
                students.Add(student);
            }
            return students;
        }
    }
}