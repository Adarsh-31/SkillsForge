﻿using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using SkillForge.Application.Common.Interfaces;
using SkillForge.Application.DTOs.Auth;
using SkillForge.Domain.Entities;

namespace SkillForge.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(IApplicationDbContext dbContext, IJwtTokenGenerator jwtTokenGenerator)
        {
            _dbContext = dbContext;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new Exception("Invalid email or password.");

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthResponse
            {
                FullName = user.FullName,
                Email = user.Email,
                Token = token
            };
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            if (await _dbContext.Users.AnyAsync(u => u.Email == request.Email))
                throw new Exception("Email already exists.");

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = "User"
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthResponse
            {
                FullName = user.FullName,
                Email = user.Email,
                Token = token
            };
        }

        public async Task<bool> ChangePasswordAsync(Guid userId, ChangePasswordRequest request)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if(user == null)
                return false;

            var isValidPassword = BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.PasswordHash);
            if(!isValidPassword) return false;

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
