namespace SkillForge.Application.DTOs.Lesson
{
    public class LessonDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Content { get; set; }
        public string? VideoUrl { get; set; }
        public Guid ModuleId { get; set; }
    }
}
