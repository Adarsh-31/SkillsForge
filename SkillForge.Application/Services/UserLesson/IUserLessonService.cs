namespace SkillForge.Application.Services.UserLesson
{
    public interface IUserLessonService
    {
        Task CompleteLessonAsync(Guid userId, Guid lessonId);
        Task<List<Guid>> GetCompletedLessonsAsync(Guid userId);
    }
}
