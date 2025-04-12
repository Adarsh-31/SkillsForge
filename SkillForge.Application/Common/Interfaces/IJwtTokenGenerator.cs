using SkillForge.Domain.Entities;

namespace SkillForge.Application.Common.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
