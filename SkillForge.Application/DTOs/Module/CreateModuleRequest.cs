namespace SkillForge.Application.DTOs.Module
{
    public class CreateModuleRequest
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Guid CourseId { get; set; }
    }
}
