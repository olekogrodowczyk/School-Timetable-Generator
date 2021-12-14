using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public static class ModelBuilderRelations
    {
        public static void InitializeRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne<Role>(x => x.Role)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TimeTable>()
                .HasOne<User>(x => x.Creator)
                .WithMany(x => x.TimeTables)
                .HasForeignKey(x => x.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Class>()
                .HasOne<TimeTable>(x => x.TimeTable)
                .WithMany(x => x.Classes)
                .HasForeignKey(x => x.TimetableId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Teacher>()
                .HasOne<Class>(x => x.Class)
                .WithOne(x => x.Teacher)
                .HasForeignKey<Class>(x => x.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Class>()
                .HasIndex(x => x.TeacherId).IsUnique(false);

            modelBuilder.Entity<Group>()
                .HasOne<Class>(x => x.Class)
                .WithMany(x => x.Groups)
                .HasForeignKey(x => x.ClassId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Assignment>()
                .HasOne<Group>(x => x.Group)
                .WithMany(x => x.Assignments)
                .HasForeignKey(x => x.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Assignment>()
                .HasOne<Student>(x => x.Student)
                .WithMany(x => x.Assignments)
                .HasForeignKey(x => x.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Lesson>()
                .HasOne<Group>(x => x.Group)
                .WithMany(x => x.Lessons)
                .HasForeignKey(x => x.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Lesson>()
                .HasOne<Subject>(x => x.Subject)
                .WithMany(x => x.Lessons)
                .HasForeignKey(x => x.SubjectId).
                OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Lesson>()
                .HasOne<Teacher>(x => x.Teacher)
                .WithMany(x => x.Lessons)
                .HasForeignKey(x => x.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Availability>()
                .HasOne<Teacher>(x => x.Teacher)
                .WithMany(x => x.Availabilities)
                .HasForeignKey(x => x.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .HasOne<Class>(x => x.Class)
                .WithMany(x => x.Students)
                .HasForeignKey(x => x.ClassId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Lesson>()
                .HasOne<Classroom>(x => x.Classroom)
                .WithMany(x => x.Lessons)
                .HasForeignKey(x => x.ClassroomId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Classroom>()
                .HasOne<TimeTable>(x => x.TimeTable)
                .WithMany(x => x.Classrooms)
                .HasForeignKey(x => x.TimetableId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Teacher>()
                .HasOne<TimeTable>(x => x.TimeTable)
                .WithMany(x => x.Teachers)
                .HasForeignKey(x => x.TimetableId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Subject>()
                .HasOne<TimeTable>(x => x.TimeTable)
                .WithMany(x => x.Subjects)
                .HasForeignKey(x => x.TimetableId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Group>()
                .HasOne<Subject>(x => x.Subject)
                .WithMany(x => x.Groups)
                .HasForeignKey(x => x.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}