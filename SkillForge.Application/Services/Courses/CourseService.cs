using Microsoft.EntityFrameworkCore;
using SkillForge.Application.Common.Interfaces;
using SkillForge.Application.DTOs.Course;
using SkillForge.Domain.Entities;

namespace SkillForge.Application.Services.Courses
{
    public class CourseService
    {
        private readonly IApplicationDbContext _context;

        public CourseService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateCourseAsync(CreateCourseRequest request)
        {
            var course = new Course
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return course.Id;
        }

        public async Task<bool> UpdateCourseAsync(Guid id, UpdateCourseRequest request)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return false;

            course.Title = request.Title ?? course.Title;
            course.Description = request.Description ?? course.Description;

            _context.Courses.Update(course);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteCourseAsync(Guid id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return false;

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<CourseDto?> GetCourseByIdAsync(Guid id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return null;

            return new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                CreatedAt = course.CreatedAt
            };
        }

        public async Task<List<CourseDto>> GetAllCoursesAsync()
        {
            return await _context.Courses
                .Select(course => new CourseDto
                {
                    Id = course.Id,
                    Title = course.Title,
                    Description = course.Description,
                    CreatedAt = course.CreatedAt
                })
                .ToListAsync();
        }
    }
}
