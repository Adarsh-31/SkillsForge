using Microsoft.EntityFrameworkCore;
using SkillForge.Domain.Entities;

namespace SkillForge.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Skill> Skills { get; }
        DbSet<Course> Courses { get; }
        DbSet<CourseSkill> CourseSkills { get; }
        DbSet<Module> Modules { get; }
        DbSet<Lesson> Lessons { get; }
        DbSet<UserCourse> UserCourses { get; }
        DbSet<UserLesson> UserLessons { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
