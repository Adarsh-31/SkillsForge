using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkillForge.Application.DTOs.Course;
using SkillForge.Application.Services.Courses;

namespace SkillForge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CourseSkillsController : ControllerBase
    {
        private readonly ICourseSkillService _courseSkillService;

        public CourseSkillsController(ICourseSkillService courseSkillService)
        {
            _courseSkillService = courseSkillService;
        }

        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddSkillToCourse([FromBody] AddCourseSkillRequest request)
        {
            await _courseSkillService.AddSkillToCourseAsync(request);
            return Ok(new { message = "Skill added to course successfully." });
        }

        [HttpPost("remove")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveSkillFromCourse([FromBody] RemoveCourseSkillRequest request)
        {
            await _courseSkillService.RemoveSkillFromCourseAsync(request);
            return Ok(new { message = "Skill removed from course successfully." });
        }

        [HttpPost("assign")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignSkillsToCourse([FromBody] AssignSkillsToCourseRequest request)
        {
            await _courseSkillService.AssignSkillsToCourseAsync(request);
            return Ok(new { message = "Skills assigned to course successfully." });
        }

        [HttpGet("course/{courseId}/skills")]
        public async Task<IActionResult> GetSkillsByCourseId(Guid courseId)
        {
            var skills = await _courseSkillService.GetSkillsByCourseIdAsync(courseId);
            return Ok(skills);
        }

        [HttpGet("skill/{skillId}/courses")]
        public async Task<IActionResult> GetCoursesBySkillId(Guid skillId)
        {
            var courses = await _courseSkillService.GetCoursesBySkillIdAsync(skillId);
            return Ok(courses);
        }

        [HttpGet("course/{courseId}/with-skills")]
        public async Task<IActionResult> GetCourseWithSkills(Guid courseId)
        {
            var course = await _courseSkillService.GetCourseWithSkillsAsync(courseId);
            if (course == null) return NotFound();
            return Ok(course);
        }
    }
}