using Microsoft.EntityFrameworkCore;
using SkillForge.Application.Common.Interfaces;
using SkillForge.Domain.Entities;

namespace SkillForge.Infrastructure.Persistence;

public class SkillForgeDbContext : DbContext,IApplicationDbContext
{
    public SkillForgeDbContext(DbContextOptions<SkillForgeDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Skill> Skills => Set<Skill>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

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
