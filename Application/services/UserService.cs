using Application.DTOs;
using Application.Interfaces;
using Domain.Models;
using Application.Services.Security; 

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _users;
        private readonly IJwtService _jwtService;  

        public UserService(IUserRepository users, IJwtService jwtService)
        {
            _users = users;
            _jwtService = jwtService;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            var email = request.Email.Trim().ToLowerInvariant();
            var username = request.Username.Trim();

            if (await _users.GetByEmailAsync(email) is not null)
                throw new InvalidOperationException("Email already in use.");

            if (await _users.GetByUsernameAsync(username) is not null)
                throw new InvalidOperationException("Username already in use.");

            var hash = PasswordHasher.Hash(request.Password);

            var user = new User
            {
                Email = email,
                Username = username,
                PasswordHash = hash
            };

            user = await _users.CreateAsync(user);

            var token = _jwtService.GenerateToken(user.Id, user.Username, user.Email);

            return new AuthResponse
            {
                UserId = user.Id,
                Email = user.Email,
                Username = user.Username,
                AccessToken = token,
                RefreshToken = null
            };
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var key = request.EmailOrUsername.Trim();
            var user = key.Contains('@')
                ? await _users.GetByEmailAsync(key.ToLowerInvariant())
                : await _users.GetByUsernameAsync(key);

            if (user is null)
                throw new InvalidOperationException("Invalid credentials.");

            var ok = PasswordHasher.Verify(request.Password, user.PasswordHash);
            if (!ok)
                throw new InvalidOperationException("Invalid credentials.");

            var token = _jwtService.GenerateToken(user.Id, user.Username, user.Email);

            return new AuthResponse
            {
                UserId = user.Id,
                Email = user.Email,
                Username = user.Username,
                AccessToken = token,
                RefreshToken = null
            };
        }
    }
}
