using Application.Common.Behaviours;
using Application.Examples.Commands.CreateExample;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UnitTests.Common.Behaviours;

public class UnhandledExceptionBehaviourTests
{
    [Fact]
    public async Task Handle_Should_CallNext_WhenNoExceptionIsThrown()
    {
        // Arrange
        var request = new CreateExampleCommand();
        var nextMock = new Mock<RequestHandlerDelegate<int>>();
        nextMock.Setup(x => x()).ReturnsAsync(new int());

        var loggerMock = new Mock<ILogger<CreateExampleCommand>>();
        var sut = new UnhandledExceptionBehaviour<CreateExampleCommand, int>(loggerMock.Object);

        // Act
        var result = await sut.Handle(request, nextMock.Object, CancellationToken.None);

        // Assert
        nextMock.Verify(x => x(), Times.Once);
        loggerMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task Handle_Should_LogAndRethrow_WhenExceptionIsThrown()
    {
        // Arrange
        var request = new CreateExampleCommand();
        var expectedException = new Exception("Test exception");
        var nextMock = new Mock<RequestHandlerDelegate<int>>();
        nextMock.Setup(x => x()).ThrowsAsync(expectedException);

        var loggerMock = new Mock<ILogger<CreateExampleCommand>>();
        var sut = new UnhandledExceptionBehaviour<CreateExampleCommand, int>(loggerMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => sut.Handle(request, nextMock.Object, CancellationToken.None));
        loggerMock.Verify(x => x.Log(
            LogLevel.Error,
            It.IsAny<EventId>(),
            It.IsAny<It.IsAnyType>(),
            expectedException,
            It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
            Times.Once);
    }
}