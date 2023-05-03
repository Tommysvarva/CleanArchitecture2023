using Application.Common.Behaviours;
using Application.Examples.Commands.CreateExample;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Application.UnitTests.Common.Behaviours;

public class LoggingBehaviourTests
{
    [Fact]
    public async Task Logs_WhenCalled()
    {
        //Arrange
        var logger = new Mock<ILogger<CreateExampleCommand>>();
        var command = new CreateExampleCommand{Title = "TestCommand1"};
        var cancellationToken = new CancellationToken();
        var loggingBehaviour = new LoggingBehaviour<CreateExampleCommand>(logger.Object);
        
        //Act
        await loggingBehaviour.Process(command, cancellationToken);
         
        //Assert
         Assert.NotEmpty(logger.Invocations);
    }
}