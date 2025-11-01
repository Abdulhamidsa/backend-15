using Api.Controllers;
using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task Search_ReturnsOk_AndCallsServiceWithCorrectArguments()
        {
            // Arrange
            var mockService = new Mock<ITitleService>();

            var fakeSearchResult = new List<TitleDto>
    {
        new TitleDto { Tconst = "tt111", PrimaryTitle = "Batman Begins" }
    };

            mockService
                .Setup(s => s.SearchTitlesAsync(5, "batman"))
                .ReturnsAsync(fakeSearchResult);

            var controller = new TitlesController(mockService.Object);

            // Act
            var actionResult = await controller.Search("batman");

            // Assert
            // 1. Ok(...)
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(200, okResult.StatusCode);

            // 2. Response body should be ApiResponse<IEnumerable<TitleDto>>
            var apiResponse = Assert.IsType<ApiResponse<IEnumerable<TitleDto>>>(okResult.Value);

            // 3. Check wrapper flags
            Assert.True(apiResponse.Success);
            Assert.Equal("Success", apiResponse.Message);

            // 4. Check data inside Data
            var data = apiResponse.Data;
            Assert.NotNull(data);
            Assert.Single(data);
            Assert.Equal("Batman Begins", data.First().PrimaryTitle);

            // 5. Verify the controller called service with the right args
            mockService.Verify(
                s => s.SearchTitlesAsync(5, "batman"),
                Times.Once
            );
        }
    }
}

