namespace SkillForge.Domain.Entities
{
    public class Tag
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<CourseTag> CourseTags { get; set; } = new List<CourseTag>();
    }
}
