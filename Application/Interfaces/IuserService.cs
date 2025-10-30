using Application.DTOs;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
    }
}
