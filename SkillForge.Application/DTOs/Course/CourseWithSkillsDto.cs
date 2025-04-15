using SkillForge.Application.DTOs.Skill;

namespace SkillForge.Application.DTOs.Course
{
    public class CourseWithSkillsDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public List<SkillDto> Skills { get; set; } = new();
    }
}
