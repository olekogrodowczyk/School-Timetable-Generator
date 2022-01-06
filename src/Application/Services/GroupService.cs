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
            var subject = await _subjectRepository.SingleOrDefaultAsync(s => s.Name == model.SubjectName && s.TimetableId == activeTimetableId);
            var teacher = await _teacherRepository.SingleOrDefaultAsync(t => t.FirstName == names[0] && t.LastName == names[1] && t.TimetableId == activeTimetableId);
            var classEntity = await _classRepository.SingleOrDefaultAsync(c => c.Name == model.ClassName && c.TimetableId == activeTimetableId);
            var students = await getStudentEntities(model.StudentIds);

            var group = new Group
            {
                SubjectId = subject.Id,
                TeacherId = teacher.Id,
                ClassId = classEntity.Id,
                Name = model.Name,
                NumberOfLessonInWeek = model.NumberOfLessonsInWeek,
                TimetableId = activeTimetableId
            };
            await _groupRepository.AddAsync(group);
            await HandleAssignmentsCreating(students, group.Id);

            return group.Id;
        }

        public async Task DeleteGroupWithAssignments(int groupId)
        {
            await _groupRepository.GetAllAsync();
            var groupEntity = await _groupRepository.GetByIdAsync(groupId);
            var subject = await _subjectRepository.SingleOrDefaultAsync(x => x.Id == groupEntity.SubjectId, x => x.Groups);
            if (subject is not null && subject.Groups.Count() == 1)
            {
                await _subjectRepository.DeleteAsync(subject.Id);
            }
            else
            {
                await _groupRepository.DeleteAsync(groupId);
            }
        }

        private async Task HandleAssignmentsCreating(IEnumerable<Student> students, int groupId)
        {
            int activeTimetableId = await _userRepository.GetCurrentActiveTimetable();
            foreach (Student student in students)
            {
                Assignment assignment = new Assignment
                {
                    StudentId = student.Id,
                    GroupId = groupId,
                    TimetableId = activeTimetableId
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