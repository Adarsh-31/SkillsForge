using System.ComponentModel.DataAnnotations;

namespace SkillForge.Domain.Entities
{
    public class UserLesson
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid LessonId { get; set; }

        public DateTime CompletedAt { get; set; } = DateTime.UtcNow;
    }
}
