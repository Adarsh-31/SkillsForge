using SkillForge.Application.DTOs.Course;
using SkillForge.Application.DTOs.Tag;

namespace SkillForge.Application.Services.Tags
{
    public interface ITagService
    {
        Task<Guid> CreateTagAsync(CreateTagRequest request);
        Task<List<TagDto>> GetAllTagsAsync();
        Task AssignTagsToCourseAsync(AssignTagsToCourseRequest request);
        Task<List<TagDto>> GetTagsByCourseIdAsync(Guid courseId);
    }
}
