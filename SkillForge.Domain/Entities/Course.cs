namespace SkillForge.Domain.Entities
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<CourseSkill> CourseSkills { get; set; } = new List<CourseSkill>();
        public ICollection<Module> Modules { get; set; } = new List<Module>();

    }
}
