using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillForge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SkillForge.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Skill> Skills { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
