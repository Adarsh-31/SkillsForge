namespace SkillForge.Application.DTOs.Lesson
{
    public class CreateLessonRequest
    {
        public string Title { get; set; } = string.Empty;
        public string? Content { get; set; }
        public string? VideoUrl { get; set; }
        public Guid ModuleId { get; set; }
    }
}
