using Microsoft.EntityFrameworkCore;
using SkillForge.Application.Common.Interfaces;
using SkillForge.Application.DTOs.Skill;
using SkillForge.Domain.Entities;

namespace SkillForge.Application.Services.Skills
{
    public class SkillService : ISkillService
    {
        private readonly IApplicationDbContext _context;

        public SkillService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateSkillAsync(CreateSkillRequest request)
        {
            var skill = new Skill
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow
            };

            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();

            return skill.Id;
        }

        public async Task<bool> UpdateSkillAsync(Guid id, UpdateSkillRequest request)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null) return false;

            skill.Name = request.Name ?? skill.Name;
            skill.Description = request.Description ?? skill.Description;

            _context.Skills.Update(skill);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSkillAsync(Guid id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null) return false;

            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<SkillDto?> GetSkillByIdAsync(Guid id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null) return null;

            return new SkillDto
            {
                Id = skill.Id,
                Name = skill.Name,
                Description = skill.Description,
                CreatedAt = skill.CreatedAt
            };
        }

        public async Task<List<SkillDto>> GetAllSkillsAsync()
        {
            return await _context.Skills
                .Select(skill => new SkillDto
                {
                    Id = skill.Id,
                    Name = skill.Name,
                    Description = skill.Description,
                    CreatedAt = skill.CreatedAt
                })
                .ToListAsync();
        }
    }
}
