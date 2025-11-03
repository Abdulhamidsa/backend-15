using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace Tests.Helpers;

public class TestApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

            if (descriptor != null)
                services.Remove(descriptor);

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Testing.json")
                .Build();

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(config.GetConnectionString("DB_CONNECTION")));

            // Ensure database exists - uses existing DB with your functions
            using var scope = services.BuildServiceProvider().CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Only ensure database exists, don't drop or recreate
            context.Database.EnsureCreated();

            // Ensure test data exists (movies for testing)
            EnsureTestDataExists(context);
        });
    }

    // Clean test data helper - only removes data, not DB structure
    public async Task CleanTestDataAsync()
    {
        using var scope = Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // Remove all users (this will cascade delete bookmarks due to FK)
        var users = await db.Users.ToListAsync();
        if (users.Any())
        {
            db.Users.RemoveRange(users);
            await db.SaveChangesAsync();
        }
    }

    // Keep the old method for backward compatibility
    public async Task CleanUsersAsync() => await CleanTestDataAsync();

    private static void EnsureTestDataExists(AppDbContext context)
    {
        // Check if we already have titles
        if (context.Titles.Any())
            return;

        // Add a minimal test movie
        var testMovie = new Domain.Models.Title
        {
            Tconst = "tt0000001",
            PrimaryTitle = "Test Movie",
            TitleType = "movie",
            IsAdult = false,
            StartYear = 2000
        };

        context.Titles.Add(testMovie);
        context.SaveChanges();
    }
}
