using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;
using Api.Middleware;
namespace Tests.Middleware;

public class ExceptionMiddlewareTests
{
    [Fact]
    public async Task InvokeAsync_NoException_CallsNextMiddleware()
    {
        var context = new DefaultHttpContext();
        var wasCalled = false;

        RequestDelegate next = (ctx) =>
        {
            wasCalled = true;
            return Task.CompletedTask;
        };

        var middleware = new ExceptionMiddleware(next);
        await middleware.InvokeAsync(context);

        Assert.True(wasCalled);
    }

    [Fact]
    public async Task InvokeAsync_WithException_ReturnsJsonError()
    {
        var context = new DefaultHttpContext();
        var responseStream = new MemoryStream();
        context.Response.Body = responseStream;

        RequestDelegate next = (ctx) =>
        {
            throw new Exception("Test error");
        };

        var middleware = new ExceptionMiddleware(next);
        await middleware.InvokeAsync(context);

        responseStream.Seek(0, SeekOrigin.Begin);
        var responseText = new StreamReader(responseStream).ReadToEnd();

        Assert.Equal("application/json", context.Response.ContentType);
        Assert.Equal(400, context.Response.StatusCode);

        var error = JsonSerializer.Deserialize<ErrorResponse>(responseText);
        Assert.False(error.success);
        Assert.Equal("Test error", error.message);
    }

    private class ErrorResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
    }
}
