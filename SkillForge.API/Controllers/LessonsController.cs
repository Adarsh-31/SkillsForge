using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillForge.Application.DTOs.Lesson;
using SkillForge.Application.Services.Lessons;

namespace SkillForge.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LessonsController : ControllerBase
{
    private readonly ILessonService _lessonService;

    public LessonsController(ILessonService lessonService)
    {
        _lessonService = lessonService;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateLesson([FromBody] CreateLessonRequest request)
    {
        var id = await _lessonService.CreateLessonAsync(request);
        return CreatedAtAction(nameof(GetLessonById), new { id }, new { id });
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateLesson(Guid id, [FromBody] UpdateLessonRequest request)
    {
        var success = await _lessonService.UpdateLessonAsync(id, request);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteLesson(Guid id)
    {
        var success = await _lessonService.DeleteLessonAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLessonById(Guid id)
    {
        var lesson = await _lessonService.GetLessonByIdAsync(id);
        if (lesson is null) return NotFound();
        return Ok(lesson);
    }

    [HttpGet("module/{moduleId}")]
    public async Task<IActionResult> GetLessonsByModule(Guid moduleId)
    {
        var lessons = await _lessonService.GetLessonsByModuleIdAsync(moduleId);
        return Ok(lessons);
    }
}
