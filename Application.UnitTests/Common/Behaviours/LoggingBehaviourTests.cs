using Application.Common.Behaviours;
using Application.Examples.Commands.CreateExample;
using Microsoft.Extensions.Logging;
namespace Application.UnitTests.Common.Behaviours;

public class LoggingBehaviourTests
{
    [Fact]
    public async Task Process_LogsInformation()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<CreateExampleCommand>>();
        var behaviour = new LoggingBehaviour<CreateExampleCommand>(loggerMock.Object);
        var request = new CreateExampleCommand();

        // Act
        await behaviour.Process(request, CancellationToken.None);

        // Assert
        Assert.Equal(LogLevel.Information, loggerMock.Invocations[0].Arguments[0]);
        Assert.Equal(new EventId(0),loggerMock.Invocations[0].Arguments[1]);
        Assert.Equal("Processed request: CreateExampleCommand CreateExampleCommand { Title =  }", loggerMock.Invocations[0].Arguments[2].ToString());
    }
}