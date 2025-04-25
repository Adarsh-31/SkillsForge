using SkillForge.Application.DTOs.Enrollment;
using SkillForge.Domain.Entities;

namespace SkillForge.Application.Services.Users
{
    public interface IUserCourseService
    {
        Task EnrollAsync(Guid userId, EnrollRequest request);
        Task UnenrollAsync(Guid userId, UnenrollRequest request);
        Task<List<Course>> GetEnrolledCoursesAsync(Guid userId);
    }
}
