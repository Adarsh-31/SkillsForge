using SkillForge.Application.DTOs.Lesson;

namespace SkillForge.Application.Services.Lessons
{
    public interface ILessonService
    {
        Task<Guid> CreateLessonAsync(CreateLessonRequest request);
        Task<bool> UpdateLessonAsync(Guid id, UpdateLessonRequest request);
        Task<bool> DeleteLessonAsync(Guid id);
        Task<LessonDto?> GetLessonByIdAsync(Guid id);
        Task<List<LessonDto>> GetLessonsByModuleIdAsync(Guid moduleId);
    }
}
