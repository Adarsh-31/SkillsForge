using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkillForge.Application.DTOs.Course;
using SkillForge.Application.DTOs.Tag;
using SkillForge.Application.Services.Tags;

namespace SkillForge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateTagRequest request)
        {
            var id = await _tagService.CreateTagAsync(request);
            return CreatedAtAction(nameof(GetAll), new { id }, new { id });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tags = await _tagService.GetAllTagsAsync();
            return Ok(tags);
        }

        [HttpPost("assign")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignTagsToCourse([FromBody] AssignTagsToCourseRequest request)
        {
            await _tagService.AssignTagsToCourseAsync(request);
            return Ok(new { message = "Tags assigned successfully." });
        }

        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetTagsByCourseId(Guid courseId)
        {
            var tags = await _tagService.GetTagsByCourseIdAsync(courseId);
            return Ok(tags);
        }
    }
}
