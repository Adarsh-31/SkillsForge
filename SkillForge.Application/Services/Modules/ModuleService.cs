using Microsoft.EntityFrameworkCore;
using SkillForge.Application.Common.Interfaces;
using SkillForge.Application.DTOs.Module;
using SkillForge.Application.Services.Modules;
using SkillForge.Domain.Entities;

namespace SkillForge.Infrastructure.Services.Modules;

public class ModuleService : IModuleService
{
    private readonly IApplicationDbContext _context;

    public ModuleService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> CreateModuleAsync(CreateModuleRequest request)
    {
        var module = new Module
        {
            Title = request.Title,
            Description = request.Description,
            CourseId = request.CourseId
        };

        _context.Modules.Add(module);
        await _context.SaveChangesAsync();
        return module.Id;
    }

    public async Task<bool> UpdateModuleAsync(Guid id, UpdateModuleRequest request)
    {
        var module = await _context.Modules.FirstOrDefaultAsync(m => m.Id == id);
        if (module is null) return false;

        module.Title = request.Title;
        module.Description = request.Description;
        module.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteModuleAsync(Guid id)
    {
        var module = await _context.Modules.FirstOrDefaultAsync(m => m.Id == id);
        if (module is null) return false;

        _context.Modules.Remove(module);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<ModuleDto?> GetModuleByIdAsync(Guid id)
    {
        return await _context.Modules
            .Where(m => m.Id == id)
            .Select(m => new ModuleDto
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                CourseId = m.CourseId
            })
            .FirstOrDefaultAsync();
    }

    public async Task<List<ModuleDto>> GetModulesByCourseIdAsync(Guid courseId)
    {
        return await _context.Modules
            .Where(m => m.CourseId == courseId)
            .Select(m => new ModuleDto
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                CourseId = m.CourseId
            }).ToListAsync();
    }
}
