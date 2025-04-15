using Microsoft.EntityFrameworkCore;
using SkillForge.Application.Common.Interfaces;
using SkillForge.Application.DTOs.Course;
using SkillForge.Application.DTOs.Skill;
using SkillForge.Domain.Entities;

namespace SkillForge.Application.Services.Courses
{
    public class CourseSkillService : ICourseSkillService
    {
        private readonly IApplicationDbContext _context;

        public CourseSkillService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddSkillToCourseAsync(AddCourseSkillRequest request)
        {
            var exists = await _context.CourseSkills
                .AnyAsync(cs => cs.CourseId == request.CourseId && cs.SkillId == request.SkillId);

            if (!exists)
            {
                var courseSkill = new CourseSkill
                {
                    CourseId = request.CourseId,
                    SkillId = request.SkillId
                };

                _context.CourseSkills.Add(courseSkill);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveSkillFromCourseAsync(RemoveCourseSkillRequest request)
        {
            var courseSkill = await _context.CourseSkills
                .FirstOrDefaultAsync(cs => cs.CourseId == request.CourseId && cs.SkillId == request.SkillId);

            if (courseSkill is not null)
            {
                _context.CourseSkills.Remove(courseSkill);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AssignSkillsToCourseAsync(AssignSkillsToCourseRequest request)
        {
            var existing = await _context.CourseSkills
                .Where(cs => cs.CourseId == request.CourseId)
                .ToListAsync();

            _context.CourseSkills.RemoveRange(existing);

            var newCourseSkills = request.SkillIds
                .Select(skillId => new CourseSkill
                {
                    CourseId = request.CourseId,
                    SkillId = skillId
                });

            _context.CourseSkills.AddRange(newCourseSkills);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SkillDto>> GetSkillsByCourseIdAsync(Guid courseId)
        {
            return await _context.CourseSkills
                .Where(cs => cs.CourseId == courseId)
                .Include(cs => cs.Skill)
                .Select(cs => new SkillDto
                {
                    Id = cs.Skill.Id,
                    Name = cs.Skill.Name,
                    Description = cs.Skill.Description,
                    CreatedAt = cs.Skill.CreatedAt
                }).ToListAsync();
        }

        public async Task<List<CourseDto>> GetCoursesBySkillIdAsync(Guid skillId)
        {
            return await _context.CourseSkills
                .Where(cs => cs.SkillId == skillId)
                .Include(cs => cs.Course)
                .Select(cs => new CourseDto
                {
                    Id = cs.Course.Id,
                    Title = cs.Course.Title,
                    Description = cs.Course.Description
                }).ToListAsync();
        }

        public async Task<CourseWithSkillsDto?> GetCourseWithSkillsAsync(Guid courseId)
        {
            var course = await _context.Courses
                .Include(c => c.CourseSkills)
                    .ThenInclude(cs => cs.Skill)
                .FirstOrDefaultAsync(c => c.Id == courseId);

            if (course == null) return null;

            return new CourseWithSkillsDto
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Skills = course.CourseSkills
                    .Select(cs => new SkillDto
                    {
                        Id = cs.Skill.Id,
                        Name = cs.Skill.Name,
                        Description = cs.Skill.Description,
                        CreatedAt = cs.Skill.CreatedAt
                    }).ToList()
            };
        }
    }
}
