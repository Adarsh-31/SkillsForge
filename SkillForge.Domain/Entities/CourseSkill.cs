using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillForge.Domain.Entities
{
    public class CourseSkill
    {
        public Guid CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public Guid SkillId { get; set; }
        public Skill Skill { get; set; } = null!;
    }
}
