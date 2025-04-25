namespace SkillForge.Domain.Entities;

public class UserCourse
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public Guid CourseId { get; set; }
    public Course Course { get; set; } = null!;
    public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
    public bool IsCompleted { get; set; } = false;
}
