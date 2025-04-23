using SkillForge.Application.DTOs.Module;

namespace SkillForge.Application.Services.Modules
{
    public interface IModuleService
    {
        Task<Guid> CreateModuleAsync(CreateModuleRequest request);
        Task<bool> UpdateModuleAsync(Guid id, UpdateModuleRequest request);
        Task<bool> DeleteModuleAsync(Guid id);
        Task<ModuleDto?> GetModuleByIdAsync(Guid id);
        Task<List<ModuleDto>> GetModulesByCourseIdAsync(Guid courseId);
    }
}
