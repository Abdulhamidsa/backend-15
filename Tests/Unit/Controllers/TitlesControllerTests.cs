using Api.Controllers;
using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Unit.Controllers
{
    public class TitlesControllerTests
    {
        [Fact]
        public async Task GetAll_ReturnsOk_WithListOfTitles()
        {
            // Arrange
            var mockService = new Mock<ITitleService>();

            var fakeTitles = new List<TitleDto>
        {
            new TitleDto { Tconst = "tt123", PrimaryTitle = "Batman" },
            new TitleDto { Tconst = "tt456", PrimaryTitle = "Joker" }
        };

            // tell the mock how to behave
            mockService
                .Setup(s => s.GetpopularTitlesAsync())
                .ReturnsAsync(fakeTitles);

            //mockService.Object gives you the “fake ITitleService”.
            //We pass that fake service into the controller constructor.
            var controller = new TitlesController(mockService.Object);

            // Act

            //actionResult is whatever the controller returned, usually an IActionResult.
            //our case, it’ll be an OkObjectResult.
            var actionResult = await controller.GetAllMovies();

            // Assert
            // 1. It should be OkObjectResult
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(200, okResult.StatusCode);

            // 2. The body should be ApiResponse<IEnumerable<TitleDto>>
            var apiResponse = Assert.IsType<ApiResponse<IEnumerable<TitleDto>>>(okResult.Value);

            // 3. Check the envelope (Success / Message)
            Assert.True(apiResponse.Success);


            // assert that here. If it just uses the default, assert "Success".
            Assert.Equal("Movies fetched", apiResponse.Message);

            // 4. Check the data inside the response
            var data = apiResponse.Data;
            Assert.NotNull(data);
            Assert.Equal(2, data.Count());

            var first = data.First();
            Assert.Equal("tt123", first.Tconst);
            Assert.Equal("Batman", first.PrimaryTitle);

            // 5. Make sure service was called once
            mockService.Verify(s => s.GetpopularTitlesAsync(), Times.Once);
        }


        [Fact]
        public async Task Search_ReturnsOk_ForValidQuery_AndAuthenticatedUser()
        {
            // Arrange
            var mockService = new Mock<ITitleService>();

            // Fake data the service will "return"
            var fakeSearchResult = new List<TitleDto>
        {
            new TitleDto { Tconst = "tt111", PrimaryTitle = "Batman Begins" }
        };

            // We expect the controller to call SearchTitlesAsync(userId: 5, q: "batman")
            mockService
                .Setup(s => s.SearchTitlesAsync(5, "batman"))
                .ReturnsAsync(fakeSearchResult);

            var controller = new TitlesController(mockService.Object);

            // Simulate an authenticated user with a valid claim.
            // our helper checks ClaimTypes.NameIdentifier first, then "sub".
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.NameIdentifier, "5")
        }, "TestAuthType"));

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = user
                }
            };

            // Act
            var actionResult = await controller.Search("batman");

            // Assert
            // Should return OkObjectResult (HTTP 200)
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(200, okResult.StatusCode);

            // Body should be ApiResponse<object>
            var apiResponse = Assert.IsType<ApiResponse<object>>(okResult.Value);

            // Check wrapper fields
            Assert.True(apiResponse.Success);
            Assert.Equal("Search completed successfully.", apiResponse.Message);

            // Data inside ApiResponse<object> should actually be our list of TitleDto
            var data = Assert.IsType<List<TitleDto>>(apiResponse.Data);
            Assert.Single(data);
            Assert.Equal("tt111", data[0].Tconst);
            Assert.Equal("Batman Begins", data[0].PrimaryTitle);

            // Make sure service was called with the same values we expected
            mockService.Verify(
                s => s.SearchTitlesAsync(5, "batman"),
                Times.Once
            );
        }

        [Fact]
        public async Task Search_ReturnsUnauthorized_WhenUserIdClaimMissing()
        {
            // Arrange
            var mockService = new Mock<ITitleService>();

            var controller = new TitlesController(mockService.Object);

            // Simulate a user with NO valid claims.
            // This will cause User.GetUserId() to throw UnauthorizedAccessException,
            // which the controller catches and turns into 401 Unauthorized.
            var userWithoutId = new ClaimsPrincipal(new ClaimsIdentity(
                claims: new Claim[] { }, // no NameIdentifier, no sub
                authenticationType: "TestAuthType"
            ));

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = userWithoutId
                }
            };

            // Act
            var actionResult = await controller.Search("batman");

            // Assert
            // We expect UnauthorizedObjectResult (HTTP 401)
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(actionResult);
            Assert.Equal(401, unauthorizedResult.StatusCode);

            // The body should be ApiResponse<string> with Success = false
            var apiResponse = Assert.IsType<ApiResponse<string>>(unauthorizedResult.Value);

            Assert.False(apiResponse.Success);
            Assert.Equal("Invalid or missing user ID claim.", apiResponse.Message);

            // Data should be default(string) which is null
            Assert.Null(apiResponse.Data);

            // Also important: service should NOT be called at all if auth fails
            mockService.Verify(
                s => s.SearchTitlesAsync(It.IsAny<long>(), It.IsAny<string>()),
                Times.Never
            );
        }
    }

}



