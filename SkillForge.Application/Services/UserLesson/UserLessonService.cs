using Microsoft.EntityFrameworkCore;
using SkillForge.Application.Common.Interfaces;
using SkillForge.Domain.Entities;

namespace SkillForge.Application.Services.UserLesson
{
    public class UserLessonService: IUserLessonService
    {
        private readonly IApplicationDbContext _dbContext;
        public UserLessonService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CompleteLessonAsync(Guid userId, Guid lessonId)
        {
            if (await _dbContext.UserLessons.AnyAsync(ul => ul.UserId == userId && ul.LessonId == lessonId))
            {
                throw new Exception("Lesson already completed.");
            }

            var userLesson = new Domain.Entities.UserLesson()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                LessonId = lessonId,
                CompletedAt = DateTime.UtcNow
            };

            _dbContext.UserLessons.Add(userLesson);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Guid>> GetCompletedLessonsAsync(Guid userId)
        {
            return await _dbContext.UserLessons
                .Where(ul => ul.UserId == userId)
                .Select(ul => ul.LessonId)
                .ToListAsync();
        }
    }
}
