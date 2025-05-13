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
        public ICollection<UserCourse> Enrollments { get; set; } = new List<UserCourse>();
        public ICollection<CourseTag> CourseTags { get; set; } = new List<CourseTag>();
    }
}
