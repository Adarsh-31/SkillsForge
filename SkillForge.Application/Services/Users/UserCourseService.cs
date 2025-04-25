using Microsoft.EntityFrameworkCore;
using SkillForge.Application.Common.Interfaces;
using SkillForge.Application.DTOs.Enrollment;
using SkillForge.Domain.Entities;

namespace SkillForge.Application.Services.Users
{
    public class UserCourseService : IUserCourseService
    {
        private readonly IApplicationDbContext _context;

        public UserCourseService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task EnrollAsync(Guid userId, EnrollRequest request)
        {
            var exists = await _context.UserCourses
                .AnyAsync(uc => uc.UserId == userId && uc.CourseId == request.CourseId);

            if (!exists)
            {
                var enrollment = new UserCourse
                {
                    UserId = userId,
                    CourseId = request.CourseId
                };

                _context.UserCourses.Add(enrollment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UnenrollAsync(Guid userId, UnenrollRequest request)
        {
            var enrollment = await _context.UserCourses
                .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.CourseId == request.CourseId);

            if (enrollment != null)
            {
                _context.UserCourses.Remove(enrollment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Course>> GetEnrolledCoursesAsync(Guid userId)
        {
            return await _context.UserCourses
                .Where(uc => uc.UserId == userId)
                .Select(uc => uc.Course)
                .ToListAsync();
        }
    }
}
