using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkillForge.Application.DTOs.Skill;
using SkillForge.Application.Services.Skills;

namespace SkillForge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillsController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSkill([FromBody] CreateSkillRequest request)
        {
            var id = await _skillService.CreateSkillAsync(request);
            return CreatedAtAction(nameof(GetSkillById), new { id }, new { id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSkill(Guid id, [FromBody] UpdateSkillRequest request)
        {
            var success = await _skillService.UpdateSkillAsync(id, request);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkill(Guid id)
        {
            var success = await _skillService.DeleteSkillAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSkillById(Guid id)
        {
            var skill = await _skillService.GetSkillByIdAsync(id);
            if (skill is null) return NotFound();
            return Ok(skill);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSkills()
        {
            var skills = await _skillService.GetAllSkillsAsync();
            return Ok(skills);
        }
    }
}
