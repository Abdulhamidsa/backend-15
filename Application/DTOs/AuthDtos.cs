namespace Application.DTOs;

public class RegisterRequest
{
    public required string Email { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}

public class LoginRequest
{
    public required string EmailOrUsername { get; set; }
    public required string Password { get; set; }
}

public class AuthResponse
{
    public long   UserId { get; set; }
    public string Email  { get; set; } = default!;
    public string Username { get; set; } = default!;

    // JWT placeholders (not implemented)
    public string? AccessToken { get; set; }   // TODO: fill when you add JWT
    public string? RefreshToken { get; set; }  // TODO: fill when you add JWT
}
