﻿namespace SkillForge.Application.DTOs.Course
{
    public class CreateCourseRequest
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
