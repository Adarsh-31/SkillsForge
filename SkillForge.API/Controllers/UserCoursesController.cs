using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkillForge.Application.DTOs.Enrollment;
using SkillForge.Application.Services.Users;

namespace SkillForge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserCoursesController : ControllerBase
    {
        private readonly IUserCourseService _userCourseService;

        public UserCoursesController(IUserCourseService userCourseService)
        {
            _userCourseService = userCourseService;
        }

        private Guid GetUserId() =>
            Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpPost("enroll")]
        public async Task<IActionResult> Enroll([FromBody] EnrollRequest request)
        {
            await _userCourseService.EnrollAsync(GetUserId(), request);
            return Ok(new { message = "Enrolled successfully." });
        }

        [HttpPost("unenroll")]
        public async Task<IActionResult> Unenroll([FromBody] UnenrollRequest request)
        {
            await _userCourseService.UnenrollAsync(GetUserId(), request);
            return Ok(new { message = "Unenrolled successfully." });
        }

        [HttpGet("my-courses")]
        public async Task<IActionResult> GetMyCourses()
        {
            var courses = await _userCourseService.GetEnrolledCoursesAsync(GetUserId());
            return Ok(courses);
        }
    }
}
