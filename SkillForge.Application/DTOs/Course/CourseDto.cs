using SkillForge.Application.DTOs.Tag;

namespace SkillForge.Application.DTOs.Course
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<TagDto> Tags { get; set; } = new();
    }
}
