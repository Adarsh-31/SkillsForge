using SkillForge.Application.DTOs.Course;
using SkillForge.Application.DTOs.Skill;

namespace SkillForge.Application.Services.Courses
{
    public interface ICourseSkillService
    {
        Task AddSkillToCourseAsync(AddCourseSkillRequest request);
        Task RemoveSkillFromCourseAsync(RemoveCourseSkillRequest request);
        Task AssignSkillsToCourseAsync(AssignSkillsToCourseRequest request); 
        Task<List<SkillDto>> GetSkillsByCourseIdAsync(Guid courseId);
        Task<List<CourseDto>> GetCoursesBySkillIdAsync(Guid skillId);
        Task<CourseWithSkillsDto?> GetCourseWithSkillsAsync(Guid courseId);
    }
}
