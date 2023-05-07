using Domain.Events;

namespace Domain.UnitTests.Events;

public class ExampleDeletedEventTests
{
    [Fact]
    public void ExampleDeletedEvent_ShouldContainDeletedExample()
    {
        // Arrange
        var createdExample = new Example();
        var exampleCreatedEvent = new ExampleDeletedEvent(createdExample);
        
        // Assert
        Assert.NotNull(exampleCreatedEvent.Example);
    }
}