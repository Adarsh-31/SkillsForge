using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillForge.Application.DTOs.Module;
using SkillForge.Application.Services.Modules;

namespace SkillForge.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CourseModulesController : ControllerBase
{
    private readonly IModuleService _moduleService;

    public CourseModulesController(IModuleService moduleService)
    {
        _moduleService = moduleService;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateModule([FromBody] CreateModuleRequest request)
    {
        var id = await _moduleService.CreateModuleAsync(request);
        return CreatedAtAction(nameof(GetModuleById), new { id }, new { id });
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateModule(Guid id, [FromBody] UpdateModuleRequest request)
    {
        var success = await _moduleService.UpdateModuleAsync(id, request);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteModule(Guid id)
    {
        var success = await _moduleService.DeleteModuleAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetModuleById(Guid id)
    {
        var module = await _moduleService.GetModuleByIdAsync(id);
        if (module is null) return NotFound();
        return Ok(module);
    }

    [HttpGet("course/{courseId}")]
    public async Task<IActionResult> GetModulesByCourse(Guid courseId)
    {
        var modules = await _moduleService.GetModulesByCourseIdAsync(courseId);
        return Ok(modules);
    }
}
