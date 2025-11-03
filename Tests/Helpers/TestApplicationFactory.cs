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

            // Ensure database and stored procedures exist
            using var scope = services.BuildServiceProvider().CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.Database.EnsureCreated();

            // Create bookmark stored procedures for testing

            // Ensure at least one test movie exists
            EnsureTestDataExists(context);
        });
    }

    public async Task CleanUsersAsync()
    {
        using var scope = Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Users.RemoveRange(db.Users);
        await db.SaveChangesAsync();
    }

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
