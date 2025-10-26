using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables
Env.Load();

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/test-db", async (AppDbContext db) =>
{
    try
    {
        await db.Database.OpenConnectionAsync();
        return "Database connection successful!";
    }
    catch (Exception ex)
    {
        return $"Connection failed: {ex.Message}";
    }
});

app.Run();
