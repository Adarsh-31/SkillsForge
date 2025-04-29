using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkillForge.Application.DTOs.UserLesson;
using SkillForge.Application.Services.UserLesson;

namespace SkillForge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserLessonsController : ControllerBase
    {
        private readonly IUserLessonService _userLessonService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserLessonsController(IUserLessonService userLessonService, IHttpContextAccessor httpContextAccessor)
        {
            _userLessonService = userLessonService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("complete")]
        public async Task<IActionResult> CompleteLesson([FromBody] CompleteLessonRequest request)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            if (userIdClaim == null)
            {
                return Unauthorized(new { message = "User not authenticated." });
            }

            var userId = Guid.Parse(userIdClaim.Value);

            await _userLessonService.CompleteLessonAsync(userId, request.LessonId);

            return Ok(new { message = "Lesson marked as completed!" });
        }


        [HttpGet("my-completed-lessons")]
        public async Task<IActionResult> GetMyCompletedLessons()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            if (userIdClaim == null)
            {
                return Unauthorized(new { message = "User not authenticated." });
            }

            var userId = Guid.Parse(userIdClaim.Value);

            var completedLessons = await _userLessonService.GetCompletedLessonsAsync(userId);

            return Ok(completedLessons);
        }

    }
}
