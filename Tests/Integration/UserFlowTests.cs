using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Api.IntegrationTests.Setup;
using Application.DTOs;


namespace Api.IntegrationTests
{
    public class UserRegistrationTests : IClassFixture<CustomWebAppFactory>
    {
        private readonly HttpClient _client;

        public UserRegistrationTests(CustomWebAppFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Should_Register_User_Successfully()
        {
            // Arrange
            var registerBody = new RegisterRequest
            {
                Email = "integrationtest@example.com",
                Username = "integrationuser",
                Password = "test123"
            };

            // Serialize manually (to preserve PascalCase)
            var json = JsonSerializer.Serialize(registerBody, new JsonSerializerOptions
            {
                PropertyNamingPolicy = null, // ensures Email/Username/Password stay PascalCase
            });

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/auth/register", content);

            var responseText = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Response JSON: " + responseText);

            // Assert
            Assert.True(response.IsSuccessStatusCode, $"❌ Register failed: {response.StatusCode} - {responseText}");
        }
    }
}
