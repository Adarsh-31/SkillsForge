using Microsoft.EntityFrameworkCore;
using SkillForge.Application.Common.Interfaces;
using SkillForge.Application.DTOs.Course;
using SkillForge.Application.DTOs.Tag;
using SkillForge.Domain.Entities;

namespace SkillForge.Application.Services.Tags
{
    public class TagService : ITagService
    {
        private readonly IApplicationDbContext _dbContext;

        public TagService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> CreateTagAsync(CreateTagRequest request)
        {
            var tag = new Tag
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description
            };

            _dbContext.Tags.Add(tag);
            await _dbContext.SaveChangesAsync();

            return tag.Id;
        }

        public async Task<List<TagDto>> GetAllTagsAsync()
        {
            return await _dbContext.Tags
                .Select(t => new TagDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description
                }).ToListAsync();
        }

        public async Task AssignTagsToCourseAsync(AssignTagsToCourseRequest request)
        {
            var existingTags = await _dbContext.CourseTags
                .Where(ct => ct.CourseId == request.CourseId)
                .ToListAsync();

            _dbContext.CourseTags.RemoveRange(existingTags);

            var newCourseTags = request.TagIds
                .Select(tagId => new CourseTag
                {
                    CourseId = request.CourseId,
                    TagId = tagId
                });

            _dbContext.CourseTags.AddRange(newCourseTags);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<TagDto>> GetTagsByCourseIdAsync(Guid courseId)
        {
            return await _dbContext.CourseTags
                .Where(ct => ct.CourseId == courseId)
                .Select(ct => new TagDto
                {
                    Id = ct.Tag.Id,
                    Name = ct.Tag.Name,
                    Description = ct.Tag.Description
                }).ToListAsync();
        }
    }
}
