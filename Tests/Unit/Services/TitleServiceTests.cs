using Application.DTOs;
using Application.Interfaces;
using Application.RowClasses;
using Application.Services;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Unit.Services
{
    public class TitleServiceTests
    {
        // 1. GetTitlesAsync should call repo with same args and return same data
        [Fact]
        public async Task GetTitlesAsync_ReturnsCatalog_FromRepository_AndPassesTypeAndGenre()
        {
            // Arrange
            var repoMock = new Mock<ITitleRepository>();

            // Fake data that the repository will "return"
            var fakeRepoResult = new List<TitleRow>
        {
            new TitleRow
            {
                Tconst = "tt999",
                PrimaryTitle = "The Dark Knight",
                StartYear = 2008,
                TitleType = "movie",
                Poster = "http://example.com/darkknight.jpg",
                Genre = "Action, Crime"
            },
            new TitleRow
            {
                Tconst = "tt888",
                PrimaryTitle = "Inception",
                StartYear = 2010,
                TitleType = "movie",
                Poster = "http://example.com/inception.jpg",
                Genre = "Sci-Fi, Thriller"
            }
        };

            // We expect the service to call the repository with the same filters
            repoMock
                .Setup(r => r.GetTitlesAsync("movie", "crime"))
                .ReturnsAsync(fakeRepoResult);

            // System under test: real service, with mocked repo
            var service = new TitleService(repoMock.Object);

            // Act
            var result = await service.GetTitlesAsync("movie", "crime");

            // Assert
            // 1. Service should give us a non-null result
            Assert.NotNull(result);

            // 2. The data we got back from the service should match
            var list = result.ToList();
            Assert.Equal(2, list.Count);

            Assert.Equal("tt999", list[0].Id);
            Assert.Equal("The Dark Knight", list[0].Title);
            Assert.Equal(2008, list[0].Year);
            Assert.Equal("movie", list[0].Type);
            Assert.Equal("http://example.com/darkknight.jpg", list[0].Poster);
            Assert.Equal("Action, Crime", list[0].Genre);

            Assert.Equal("tt888", list[1].Id);
            Assert.Equal("Inception", list[1].Title);
            Assert.Equal(2010, list[1].Year);
            Assert.Equal("movie", list[1].Type);
            Assert.Equal("http://example.com/inception.jpg", list[1].Poster);
            Assert.Equal("Sci-Fi, Thriller", list[1].Genre);

            // 3. Verify repo was called exactly once with those same arguments
            repoMock.Verify(
                r => r.GetTitlesAsync("movie", "crime"),
                Times.Once
            );
        }


        // 2. SearchTitlesAsync should call repo with userId + query and return mapped results
        [Fact]
        public async Task SearchTitlesAsync_ReturnsMappedDtos_AndPassesWildcardQueryToRepository()
        {
            // Arrange
            var repoMock = new Mock<ITitleRepository>();

            // This is what the repository would normally return from the DB.
            // It's a list of domain entities (Title), not DTOs.
            var fakeRepoResult = new List<Title>
        {
            new Title
            {
                Tconst = "tt111",
                PrimaryTitle = "Batman Begins",
                StartYear = 2005,
                TitleType = "movie",
                Poster = "https://example.com/begins.jpg"
            },
            new Title
            {
                Tconst = "tt222",
                PrimaryTitle = "The Batman",
                StartYear = 2022,
                TitleType = "movie",
                Poster = "https://example.com/thebatman.jpg"
            }
        };

            // When the service calls the repository with:
            // userId = 5
            // query = "%batman%"
            // ...we want the repo to return fakeRepoResult
            repoMock
                .Setup(r => r.SearchTitlesAsync(5, "%batman%"))
                .ReturnsAsync(fakeRepoResult);

            // System under test: REAL service, FAKE repo
            var service = new TitleService(repoMock.Object);

            // Act
            var result = await service.SearchTitlesAsync(5, "batman");

            // Assert

            // 1. Service should give us something, not null
            Assert.NotNull(result);

            // 2. Convert to list so we can inspect
            var list = result.ToList();
            Assert.Equal(2, list.Count);

            // 3. Check first mapped DTO
            Assert.Equal("tt111", list[0].Tconst);
            Assert.Equal("Batman Begins", list[0].PrimaryTitle);
            Assert.Equal(2005, list[0].StartYear);
            Assert.Equal("movie", list[0].TitleType);
            Assert.Equal("https://example.com/begins.jpg", list[0].Poster);

            // 4. Check second mapped DTO
            Assert.Equal("tt222", list[1].Tconst);
            Assert.Equal("The Batman", list[1].PrimaryTitle);
            Assert.Equal(2022, list[1].StartYear);
            Assert.Equal("movie", list[1].TitleType);
            Assert.Equal("https://example.com/thebatman.jpg", list[1].Poster);

            // 5. Verify that the service called the repo with the wildcard query
            repoMock.Verify(
                r => r.SearchTitlesAsync(5, "%batman%"),
                Times.Once
            );
        }

        [Fact]
        public async Task GetTitlesAsync_WhenRepoReturnsEmpty_ReturnsEmptyList()
        {
            // Arrange
            var repoMock = new Mock<ITitleRepository>();

            // Repo returns nothing for this filter
            repoMock
                .Setup(r => r.GetTitlesAsync("series", "drama"))
                .ReturnsAsync(new List<TitleRow>());

            var service = new TitleService(repoMock.Object);

            // Act
            var result = await service.GetTitlesAsync("series", "drama");

            // Assert
            //Checks that the service actually returned an object.
            Assert.NotNull(result);       // should not be null
            Assert.Empty(result);         // should be empty, not throw

            // Repo should have been called exactly once with same values
            repoMock.Verify(
                r => r.GetTitlesAsync("series", "drama"),
                Times.Once
            );
        }
    }

}