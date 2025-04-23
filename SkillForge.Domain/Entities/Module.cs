namespace SkillForge.Domain.Entities;

public class Module
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }

    // Foreign Key
    public Guid CourseId { get; set; }
    public Course Course { get; set; } = null!;

    // Navigation
    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    // Optional metadata
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
}
