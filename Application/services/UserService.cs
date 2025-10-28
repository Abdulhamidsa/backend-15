using Application.DTOs;
using Application.Interfaces;
using Application.Services.Security;
using Domain.Models;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _users;

    public UserService(IUserRepository users) => _users = users;

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        // Normalize inputs
        var email = request.Email.Trim().ToLowerInvariant();
        var username = request.Username.Trim();

        // Check if email or username already exists
        if (await _users.GetByEmailAsync(email) is not null)
            throw new InvalidOperationException("Email already in use.");

        if (await _users.GetByUsernameAsync(username) is not null)
            throw new InvalidOperationException("Username already in use.");

        // Hash password (BCrypt handles salt internally)
        var hash = PasswordHasher.Hash(request.Password);

        var user = new User
        {
            Email = email,
            Username = username,
            PasswordHash = hash
        };

        user = await _users.CreateAsync(user);

        // TODO: Generate JWT here later
        return new AuthResponse
        {
            UserId = user.Id,
            Email = user.Email,
            Username = user.Username,
            AccessToken = null,   // to be implemented
            RefreshToken = null   // to be implemented
        };
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        // Find user by email or username
        var key = request.EmailOrUsername.Trim();
        var user = key.Contains('@')
            ? await _users.GetByEmailAsync(key.ToLowerInvariant())
            : await _users.GetByUsernameAsync(key);

        if (user is null)
            throw new InvalidOperationException("Invalid credentials.");

        // Verify password
        var ok = PasswordHasher.Verify(request.Password, user.PasswordHash);
        if (!ok)
            throw new InvalidOperationException("Invalid credentials.");

        // TODO: Generate JWT here later
        return new AuthResponse
        {
            UserId = user.Id,
            Email = user.Email,
            Username = user.Username,
            AccessToken = null,
            RefreshToken = null
        };
    }
}
