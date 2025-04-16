using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillForge.Application.DTOs.User;

namespace SkillForge.Application.Services.Users
{
    public interface IUserService
    {
        Task<Guid> CreateUserAsync(CreateUserRequest request);
        Task<bool> UpdateUserAsync(Guid id, UpdateUserRequest request);
        Task<bool> DeleteUserAsync(Guid id);
        Task<UserDto?> GetUserByIdAsync(Guid id);
        Task<List<UserDto>> GetAllUsersAsync();
    }
}
