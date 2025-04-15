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
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
