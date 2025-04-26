using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillForge.Application.DTOs.Course;
using SkillForge.Application.Services.Courses;

namespace SkillForge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseRequest request)
        {
            var id = await _courseService.CreateCourseAsync(request);
            return CreatedAtAction(nameof(GetCourseById), new { id }, new { id });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCourse(Guid id, [FromBody] UpdateCourseRequest request)
        {
            var success = await _courseService.UpdateCourseAsync(id, request);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            var success = await _courseService.DeleteCourseAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(Guid id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course is null) return NotFound();
            return Ok(course);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(courses);
        }
    }
}
