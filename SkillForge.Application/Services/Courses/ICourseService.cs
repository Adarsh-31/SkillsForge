using SkillForge.Application.DTOs.Course;

namespace SkillForge.Application.Services.Courses
{
    public interface ICourseService
    {
        Task<Guid> CreateCourseAsync(CreateCourseRequest request);
        Task<bool> UpdateCourseAsync(Guid id, UpdateCourseRequest request);
        Task<bool> DeleteCourseAsync(Guid id);
        Task<CourseDto?> GetCourseByIdAsync(Guid id);
        Task<List<CourseDto>> GetAllCoursesAsync();
    }
}
