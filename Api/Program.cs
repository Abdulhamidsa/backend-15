using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Application.Interfaces;
using Infrastructure.Repositories;
using Application.Services;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Load .env
Env.Load();
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");

// Register DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Register Repository and Service
builder.Services.AddScoped<ITitleRepository, TitleRepository>();
builder.Services.AddScoped<ITitleService, TitleService>();

// Users
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();





builder.Services.AddControllers();

var app = builder.Build();
// app.UseMiddleware<Api.Middleware.ExceptionMiddleware>();
app.MapControllers();
app.Run();
