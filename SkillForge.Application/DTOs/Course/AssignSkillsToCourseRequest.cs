namespace SkillForge.Application.DTOs.Course
{
    public class AssignSkillsToCourseRequest
    {
        public Guid CourseId { get; set; }
        public List<Guid> SkillIds { get; set; } = new();
    }
}
