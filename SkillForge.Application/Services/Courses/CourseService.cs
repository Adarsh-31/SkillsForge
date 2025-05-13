using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SkillForge.Application.Common.Interfaces;
using SkillForge.Application.DTOs.Course;
using SkillForge.Application.DTOs.Lesson;
using SkillForge.Application.DTOs.Tag;
using SkillForge.Domain.Entities;

namespace SkillForge.Application.Services.Courses
{
    public class CourseService : ICourseService
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
                .Include(c => c.CourseTags)
                    .ThenInclude(ct => ct.Tag)
                .Select(course => new CourseDto
                {
                    Id = course.Id,
                    Title = course.Title,
                    Description = course.Description,
                    CreatedAt = course.CreatedAt,
                    Tags = course.CourseTags.Select(ct => new TagDto
                    {
                        Id = ct.Tag.Id,
                        Name = ct.Tag.Name,
                        Description = ct.Tag.Description
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<List<CourseDto>> GetCoursesByTagAsync(Guid tagId)
        {
            return await _context.Courses
                .Where(c => c.CourseTags.Any(ct => ct.TagId == tagId))
                .Include(c => c.CourseTags)
                    .ThenInclude(ct => ct.Tag)
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    CreatedAt = c.CreatedAt,
                    Tags = c.CourseTags.Select(ct => new TagDto
                    {
                        Id = ct.Tag.Id,
                        Name = ct.Tag.Name,
                        Description = ct.Tag.Description
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<List<CourseDto>> GetRelatedCoursesAsync(Guid courseId)
        {
            // Step 1: Get all TagIds assigned to the current course
            var tagIds = await _context.CourseTags
                .Where(ct => ct.CourseId == courseId)
                .Select(ct => ct.TagId)
                .ToListAsync();

            if (!tagIds.Any()) return new List<CourseDto>();

            // Step 2: Get CourseIds of other courses sharing those tags
            var relatedCourseIds = await _context.CourseTags
                .Where(ct => tagIds.Contains(ct.TagId) && ct.CourseId != courseId)
                .Select(ct => ct.CourseId)
                .Distinct()
                .ToListAsync();

            // Step 3: Load related course details
            var relatedCourses = await _context.Courses
                .Where(c => relatedCourseIds.Contains(c.Id))
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    CreatedAt = c.CreatedAt
                })
                .ToListAsync();

            return relatedCourses;
        }

    }
}
