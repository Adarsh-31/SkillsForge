using Microsoft.EntityFrameworkCore;
using SkillForge.Application.Common.Interfaces;
using SkillForge.Application.DTOs.Lesson;
using SkillForge.Application.Services.Lessons;
using SkillForge.Domain.Entities;

namespace SkillForge.Infrastructure.Services.Lessons;

public class LessonService : ILessonService
{
    private readonly IApplicationDbContext _context;

    public LessonService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> CreateLessonAsync(CreateLessonRequest request)
    {
        var lesson = new Lesson
        {
            Title = request.Title,
            Content = request.Content,
            VideoUrl = request.VideoUrl,
            ModuleId = request.ModuleId
        };

        _context.Lessons.Add(lesson);
        await _context.SaveChangesAsync();
        return lesson.Id;
    }

    public async Task<bool> UpdateLessonAsync(Guid id, UpdateLessonRequest request)
    {
        var lesson = await _context.Lessons.FirstOrDefaultAsync(l => l.Id == id);
        if (lesson is null) return false;

        lesson.Title = request.Title;
        lesson.Content = request.Content;
        lesson.VideoUrl = request.VideoUrl;
        lesson.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteLessonAsync(Guid id)
    {
        var lesson = await _context.Lessons.FirstOrDefaultAsync(l => l.Id == id);
        if (lesson is null) return false;

        _context.Lessons.Remove(lesson);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<LessonDto?> GetLessonByIdAsync(Guid id)
    {
        return await _context.Lessons
            .Where(l => l.Id == id)
            .Select(l => new LessonDto
            {
                Id = l.Id,
                Title = l.Title,
                Content = l.Content,
                VideoUrl = l.VideoUrl,
                ModuleId = l.ModuleId
            }).FirstOrDefaultAsync();
    }

    public async Task<List<LessonDto>> GetLessonsByModuleIdAsync(Guid moduleId)
    {
        return await _context.Lessons
            .Where(l => l.ModuleId == moduleId)
            .Select(l => new LessonDto
            {
                Id = l.Id,
                Title = l.Title,
                Content = l.Content,
                VideoUrl = l.VideoUrl,
                ModuleId = l.ModuleId
            }).ToListAsync();
    }
}
