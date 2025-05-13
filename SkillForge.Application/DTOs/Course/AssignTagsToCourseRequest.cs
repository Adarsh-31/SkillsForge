namespace SkillForge.Application.DTOs.Course
{
    public class AssignTagsToCourseRequest
    {
        public Guid CourseId { get; set; }
        public List<Guid> TagIds { get; set; } = new();
    }
}
