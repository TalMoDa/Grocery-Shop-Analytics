using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopAnalytics.Common.Models.ResultPattern;
using ShopAnalytics.Controllers;
using Xunit;

namespace ShopAnalyticsTests.ControllersTests
{
    public class AppBaseControllerTests
    {
        private readonly TestableAppBaseController _controller;
        private readonly HttpContext _httpContext;
        
        public AppBaseControllerTests()
        {
            _controller = new TestableAppBaseController();
            _httpContext = new DefaultHttpContext();
            _controller.ControllerContext = new ControllerContext { HttpContext = _httpContext };
        }

        [Fact]
        public void ResultOf_SuccessResult_ReturnsOk()
        {
            // Arrange
            var expectedValue = "Test Success";
            var result = Result<string>.Success(expectedValue);

            // Act
            var response = _controller.ResultOf(result);

            // Assert
            response.Should().BeOfType<OkObjectResult>();
            var okResult = response as OkObjectResult;
            okResult!.Value.Should().Be(expectedValue);
        }

        [Fact]
        public void ResultOf_ErrorResult_ReturnsProblemDetails()
        {
            // Arrange
            var error = Error.BadRequest("Test error message", "ERR001");
            var result = Result<string>.Failure([error]);
            
            // Act
            var response = _controller.ResultOf(result);

            // Assert
            response.Should().BeOfType<ObjectResult>();

            var objectResult = response as ObjectResult;
            objectResult!.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            objectResult.Value.Should().BeOfType<ProblemDetails>();

            var problemDetails = objectResult.Value as ProblemDetails;
            problemDetails!.Title.Should().Be("Bad Request");
            problemDetails.Detail.Should().Be(error.Message);
        }

        [Fact]
        public void ResultOf_MultipleErrors_ReturnsProblemDetailsWithHighestStatusCode()
        {
            // Arrange
            var errors = new List<Error>
            {
                Error.BadRequest("Low priority error", "ERR_LOW"),
                Error.InternalServerError("Critical error", "ERR_CRIT")
            };
            var result = Result<string>.Failure(errors);

            // Act
            var response = _controller.ResultOf(result);

            // Assert
            response.Should().BeOfType<ObjectResult>();

            var objectResult = response as ObjectResult;
            objectResult!.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);

            var problemDetails = objectResult.Value as ProblemDetails;
            problemDetails!.Title.Should().Be("Internal Server Error");
            problemDetails.Detail.Should().Contain("Critical error").And.Contain("Low priority error");
        }
    }

    /// <summary>
    /// A testable version of AppBaseController to allow instantiation.
    /// </summary>
    public class TestableAppBaseController : AppBaseController
    {
        // Expose the protected method for testing
        public new IActionResult ResultOf<T>(Result<T> result) => base.ResultOf(result);
    }
}
