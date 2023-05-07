using Domain.Events;

namespace Domain.UnitTests.Events;

public class ExampleCreatedEventTests
{
    [Fact]
    public void ExampleCreatedEvent_ShouldContainCreatedExample()
    {
        // Arrange
        var createdExample = new Example();
        var exampleCreatedEvent = new ExampleCreatedEvent(createdExample);
        
        // Assert
        Assert.NotNull(exampleCreatedEvent.Example);
    }
}