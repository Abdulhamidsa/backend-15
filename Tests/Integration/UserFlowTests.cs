using System.Net;
using System.Net.Http.Json;
using Application.DTOs;
using Application.Common;
using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Tests.Helpers;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Tests.Integration;

// Integration tests written by a student for learning purposes
// This file tests the entire user flow — from registration and login,
// all the way to adding and removing bookmarks in the database.

public class UserFlowTests : IClassFixture<TestApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly TestApplicationFactory _factory;

    public UserFlowTests(TestApplicationFactory factory)
    {
        // the factory spins up a real copy of our API (but in test mode)
        _factory = factory;
        _client = factory.CreateClient();
    }

    // helper function that registers and logs in a user, then sets the JWT header automatically

    private async Task<string> AuthenticateAsync(string email, string username)
    {
        // register a user
        var reg = new RegisterRequest { Email = email, Username = username, Password = "Secret123" };
        await _client.PostAsJsonAsync("/api/auth/register", reg);

        // login with the same credentials
        var login = new LoginRequest { EmailOrUsername = email, Password = "Secret123" };
        var loginRes = await _client.PostAsJsonAsync("/api/auth/login", login);
        var body = await loginRes.Content.ReadFromJsonAsync<ApiResponse<AuthResponse>>();

        // extract JWT token
        var token = body!.Data!.AccessToken!;

        // attach the token to the HTTP client so future requests are authenticated
        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        return token;
    }

    // 🎬 get any movie id from the database so we can test bookmarking
    private async Task<string> GetAnyMovieIdAsync()
    {
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // pick the first movie from the Titles table (assuming test db is already seeded)
        var movie = await db.Titles
            .Select(t => t.Tconst)
            .FirstOrDefaultAsync();

        // if there are no movies, the test should fail so we know something’s wrong with setup
        if (movie == null)
            throw new InvalidOperationException("⚠️ Test database has no movies. Seed at least one title before running tests.");

        return movie;
    }

    [Fact(DisplayName = "User flow: Register → Login → Bookmark → Remove")]
    public async Task User_Full_Flow_Works()
    {
        // clean previous test users so results are consistent
        await _factory.CleanUsersAsync();

        // grab any movie for bookmark testing
        var movieId = await GetAnyMovieIdAsync();

        // create and log in the user
        await AuthenticateAsync("flow@test.com", "flowuser");

        // Add bookmark
        var addRes = await _client.PostAsync($"/api/bookmarks/toggle/{movieId}", null);
        var addBody = await addRes.Content.ReadFromJsonAsync<ApiResponse<object>>();
        addRes.StatusCode.Should().Be(HttpStatusCode.OK);
        addBody!.Message.Should().Be("Bookmark added");

        // verify it really exists in database
        using (var scope = _factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Bookmarks.Any(b => b.Tconst == movieId).Should().BeTrue();
        }

        // 2Remove bookmark
        var removeRes = await _client.PostAsync($"/api/bookmarks/toggle/{movieId}", null);
        var removeBody = await removeRes.Content.ReadFromJsonAsync<ApiResponse<object>>();
        removeRes.StatusCode.Should().Be(HttpStatusCode.OK);
        removeBody!.Message.Should().Be("Bookmark removed");

        // make sure it’s gone from DB
        using (var scope = _factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Bookmarks.Any(b => b.Tconst == movieId).Should().BeFalse();
        }

        // Add again and test GET endpoint
        await _client.PostAsync($"/api/bookmarks/toggle/{movieId}", null);
        var getRes = await _client.GetAsync("/api/bookmarks");
        var getBody = await getRes.Content.ReadFromJsonAsync<ApiResponse<List<object>>>();
        getRes.StatusCode.Should().Be(HttpStatusCode.OK);
        getBody!.Data.Should().HaveCount(1);
    }

    [Fact(DisplayName = "Unauthorized bookmark should fail")]
    public async Task Bookmark_Without_Auth_Should_Fail()
    {
        // we don’t authenticate here on purpose to make sure
        // that unauthorized users can’t create bookmarks
        await _factory.CleanUsersAsync();
        var movieId = await GetAnyMovieIdAsync();

        var res = await _client.PostAsync($"/api/bookmarks/toggle/{movieId}", null);

        // should return 401 Unauthorized since no token is provided
        res.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}