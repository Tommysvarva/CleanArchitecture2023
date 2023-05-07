using Application.Common.Exceptions;
using FluentValidation.Results;
using Xunit;

namespace Application.UnitTests.Common.Exceptions;

public class ValidationExceptionTests
{
    [Fact]
    public void Constructor_Default_SetsErrorMessage()
    {
        // Arrange and Act
        var exception = new ValidationException();

        // Assert
        Assert.Equal("One or more validation failures have occurred.", exception.Message);
    }

    [Fact]
    public void Constructor_Failures_SetsErrors()
    {
        // Arrange
        var failures = new List<ValidationFailure>
        {
            new ValidationFailure("Name", "Name is required."),
            new ValidationFailure("Email", "Email is not valid."),
            new ValidationFailure("Email", "Email is required.")
        };

        // Act
        var exception = new ValidationException(failures);

        // Assert
        Assert.Equal(2, exception.Errors.Count); 
        Assert.Single(exception.Errors["Name"]); 
        Assert.Equal(2, exception.Errors["Email"].Length);
    }
}