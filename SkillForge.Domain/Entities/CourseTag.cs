namespace SkillForge.Domain.Entities
{
    public class CourseTag
    {
        public Guid CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public Guid TagId { get; set; }
        public Tag Tag { get; set; } = null!;
    }
}
