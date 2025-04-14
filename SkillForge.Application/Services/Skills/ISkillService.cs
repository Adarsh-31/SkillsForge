using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillForge.Application.DTOs.Skill;

namespace SkillForge.Application.Services.Skills
{
    public interface ISkillService
    {
        Task<Guid> CreateSkillAsync(CreateSkillRequest request);
        Task<bool> UpdateSkillAsync(Guid id, UpdateSkillRequest request);
        Task<bool> DeleteSkillAsync(Guid id);
        Task<SkillDto?> GetSkillByIdAsync(Guid id);
        Task<List<SkillDto>> GetAllSkillsAsync();
    }
}
