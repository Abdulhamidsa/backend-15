using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Application.Interfaces;
using Infrastructure.Repositories;
using Application.Services;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using DotNetEnv;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<ITitleRepository, TitleRepository>();
builder.Services.AddScoped<ITitleService, TitleService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBookmarkRepository, BookmarkRepository>();
builder.Services.AddScoped<IBookmarkService, BookmarkService>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.Configure<JwtSettings>(options =>
{
    options.SecretKey = Environment.GetEnvironmentVariable("JWT_SECRET")!;
    options.Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER")!;
    options.Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE")!;
    options.ExpiryMinutes = int.Parse(Environment.GetEnvironmentVariable("JWT_EXPIRY_MINUTES")!);
});

builder.Services.AddSingleton<IJwtService, JwtService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>()
        ?? new JwtSettings
        {
            SecretKey = Environment.GetEnvironmentVariable("JWT_SECRET")!,
            Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER")!,
            Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE")!,
            ExpiryMinutes = int.Parse(Environment.GetEnvironmentVariable("JWT_EXPIRY_MINUTES")!)
        };

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
    };
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseMiddleware<Api.Middleware.ExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();

public partial class Program { }

