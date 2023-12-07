using Application.Common.Behaviours;
using Application.Examples.Commands.CreateExample;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Application.UnitTests.Common.Behaviours;

public class ValidationBehaviourTests
{
    [Fact]
    public async Task Handle_NoValidators_CallsNext()
    {
        // Arrange
        var request = new CreateExampleCommand();
        var cancellationToken = CancellationToken.None;
        var nextMock = new Mock<RequestHandlerDelegate<int>>();
        nextMock.Setup(next => next()).ReturnsAsync(new int());
        var validators = Enumerable.Empty<IValidator<CreateExampleCommand>>();
        var validationBehaviour = new ValidationBehaviour<CreateExampleCommand, int>(validators);

        // Act
        var result = await validationBehaviour.Handle(request, nextMock.Object, cancellationToken);

        // Assert
        nextMock.Verify(next => next(), Times.Once);
        Assert.IsType<int>(result);
    }

    [Fact]
    public async Task Handle_WithValidators_ValidRequest_CallsNext()
    {
        // Arrange
        var request = new CreateExampleCommand();
        var cancellationToken = CancellationToken.None;
        var nextMock = new Mock<RequestHandlerDelegate<int>>();
        nextMock.Setup(next => next()).ReturnsAsync(new int());
        var validatorMock = new Mock<IValidator<CreateExampleCommand>>();
        validatorMock.Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<CreateExampleCommand>>(), cancellationToken))
            .ReturnsAsync(new ValidationResult());
        var validators = new[] { validatorMock.Object };
        var validationBehaviour = new ValidationBehaviour<CreateExampleCommand, int>(validators);

        // Act
        var result = await validationBehaviour.Handle(request, nextMock.Object, cancellationToken);

        // Assert
        nextMock.Verify(next => next(), Times.Once);
        Assert.IsType<int>(result);
    }

    [Fact]
    public async Task Handle_WithValidators_InvalidRequest_ThrowsValidationException()
    {
        // Arrange
        var request = new CreateExampleCommand();
        var cancellationToken = CancellationToken.None;
        var nextMock = new Mock<RequestHandlerDelegate<int>>();
        var validatorMock = new Mock<IValidator<CreateExampleCommand>>();
        validatorMock.Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<CreateExampleCommand>>(), cancellationToken))
            .ReturnsAsync(new ValidationResult(new[] { new ValidationFailure("propertyName", "errorMessage") }));
        var validators = new[] { validatorMock.Object };
        var validationBehaviour = new ValidationBehaviour<CreateExampleCommand, int>(validators);

        // Act & Assert
        await Assert.ThrowsAsync<Application.Common.Exceptions.ValidationException>(
            async () => await validationBehaviour.Handle(request, nextMock.Object, cancellationToken));

        nextMock.Verify(next => next(), Times.Never);
    }
}