using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Xunit;

namespace Tests.Unit.Helpers
{
    public class ApiResponseTests
    {
        [Fact]
        public void Ok_ShouldReturnSuccessTrue_AndMessageAndData()
        {
            // Arrange
            var expectedData = new { Id = 1, Name = "Test" };
            var expectedMessage = "Operation successful";

            // Act
            var response = ApiResponse<object>.Ok(expectedData, expectedMessage);

            // Assert
            Assert.True(response.Success);
            Assert.Equal(expectedMessage, response.Message);
            Assert.Equal(expectedData, response.Data);
        }

        [Fact]
        public void Ok_ShouldUseDefaultMessage_WhenMessageNotProvided()
        {
            // Arrange
            var expectedData = 123;

            // Act
            var response = ApiResponse<int>.Ok(expectedData);

            // Assert
            Assert.True(response.Success);
            Assert.Equal("Success", response.Message);
            Assert.Equal(expectedData, response.Data);
        }

        [Fact]
        public void Fail_ShouldReturnSuccessFalse_AndSetMessage()
        {
            // Arrange
            var expectedMessage = "Something went wrong";

            // Act
            var response = ApiResponse<string>.Fail(expectedMessage);

            // Assert
            Assert.False(response.Success);
            Assert.Equal(expectedMessage, response.Message);
            Assert.Null(response.Data);
        }

        [Fact]
        public void Fail_ShouldSetDataToDefault_ForValueType()
        {
            // Arrange
            var expectedMessage = "Error occurred";

            // Act
            var response = ApiResponse<int>.Fail(expectedMessage);

            // Assert
            Assert.False(response.Success);
            Assert.Equal(expectedMessage, response.Message);
            Assert.Equal(default(int), response.Data);
        }
    }
}
