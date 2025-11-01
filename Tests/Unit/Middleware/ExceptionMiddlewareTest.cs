using System.Net;
using System.Text.Json;
using Api.Middleware;

namespace Api.Tests

{
    public class ExceptionMiddlewareTests
    {
        [Fact]
        public async Task InvokeAsync_NoException_CallsNextDelegate()
        {
            var context = new DefaultHttpContext();
            var wasCalled = false;

            RequestDelegate next = ctx =>
            {
                wasCalled = true;
                return Task.CompletedTask;
            };

            var middleware = new ExceptionMiddleware(next);

            await middleware.InvokeAsync(context);

            Assert.True(wasCalled);
        }

        [Fact]
        public async Task InvokeAsync_WhenExceptionThrown_ReturnsBadRequestWithJson()
        {
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            RequestDelegate next = ctx =>
            {
                throw new Exception("Something went wrong");
            };

            var middleware = new ExceptionMiddleware(next);

            await middleware.InvokeAsync(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var bodyText = new StreamReader(context.Response.Body).ReadToEnd();

            var response = JsonSerializer.Deserialize<JsonElement>(bodyText);
            var message = response.GetProperty("message").GetString();
            var success = response.GetProperty("success").GetBoolean();

            Assert.Equal((int)HttpStatusCode.BadRequest, context.Response.StatusCode);
            Assert.False(success);
            Assert.Equal("Something went wrong", message);
        }
    }
}

