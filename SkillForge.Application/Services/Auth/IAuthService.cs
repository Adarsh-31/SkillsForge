using SkillForge.Application.DTOs.Auth;

namespace SkillForge.Application.Services.Auth
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<bool> ChangePasswordAsync(Guid userId, ChangePasswordRequest request);
    }
}
