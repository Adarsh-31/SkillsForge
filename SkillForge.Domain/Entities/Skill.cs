﻿namespace SkillForge.Domain.Entities
{
    public class Skill
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<CourseSkill> CourseSkills { get; set; } = new List<CourseSkill>();
    }
}
