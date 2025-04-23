namespace SkillForge.Application.DTOs.Lesson
{
    public class UpdateLessonRequest
    {
        public string Title { get; set; } = string.Empty;
        public string? Content { get; set; }
        public string? VideoUrl { get; set; }
    }
}
