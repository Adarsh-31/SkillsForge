﻿namespace SkillForge.Application.DTOs.Tag
{
    public class TagDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
