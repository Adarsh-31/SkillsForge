using Microsoft.EntityFrameworkCore;
using SkillForge.Application.Common.Interfaces;
using SkillForge.Domain.Entities;

namespace SkillForge.Infrastructure.Persistence;

public class SkillForgeDbContext : DbContext, IApplicationDbContext
{
    public SkillForgeDbContext(DbContextOptions<SkillForgeDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<CourseSkill> CourseSkills => Set<CourseSkill>();
    public DbSet<Module> Modules => Set<Module>();
    public DbSet<Lesson> Lessons => Set<Lesson>();
    public DbSet<UserCourse> UserCourses => Set<UserCourse>();
    public DbSet<UserLesson> UserLessons => Set<UserLesson>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

        modelBuilder.Entity<CourseSkill>()
        .HasKey(cs => new { cs.CourseId, cs.SkillId });

        modelBuilder.Entity<CourseSkill>()
            .HasOne(cs => cs.Course)
            .WithMany(c => c.CourseSkills)
            .HasForeignKey(cs => cs.CourseId);

        modelBuilder.Entity<CourseSkill>()
            .HasOne(cs => cs.Skill)
            .WithMany(s => s.CourseSkills)
            .HasForeignKey(cs => cs.SkillId);

        modelBuilder.Entity<Course>()
            .HasMany(c => c.Modules)
            .WithOne(m => m.Course)
            .HasForeignKey(m => m.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Module>()
            .HasMany(m => m.Lessons)
            .WithOne(l => l.Module)
            .HasForeignKey(l => l.ModuleId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserCourse>()
            .HasKey(uc => new { uc.UserId, uc.CourseId });

        modelBuilder.Entity<UserCourse>()
            .HasOne(uc => uc.User)
            .WithMany(u => u.Enrollments)
            .HasForeignKey(uc => uc.UserId);

        modelBuilder.Entity<UserCourse>()
            .HasOne(uc => uc.Course)
            .WithMany(c => c.Enrollments)
            .HasForeignKey(uc => uc.CourseId);


        modelBuilder.Entity<User>().HasData(new User
        {
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            FullName = "Admin User",
            Email = "admin@skillforge.com",
            PasswordHash = "$2a$12$uRghXwlZ16cOP/HxJ8eV8OR4R9LJgra5NwAN4SDaG5d/pIymxswMu",
            Role = "Admin",
            CreatedAt = new DateTime(2025, 4, 14, 10, 7, 22, 588, DateTimeKind.Utc)
        });
    }
}
