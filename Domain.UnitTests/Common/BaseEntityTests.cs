using Domain.Events;

namespace Domain.UnitTests.Common;

public class BaseEntityTests
{
    
    [Fact]
    public void EntityThatInheritsFromBaseEntity_ShouldHaveIdField()
    {
        // Arrange
        var entity = new Example{Title = "test"};
        var domainEvent = new ExampleCreatedEvent(entity);
        
        // Act
        entity.AddDomainEvent(domainEvent);

        // Assert
        Assert.Contains(domainEvent, entity.DomainEvents);
        Assert.Equal(0, entity.Id);
    }

    
    [Fact]
    public void AddDomainEvent_ShouldAddEventToDomainEventsList()
    {
        // Arrange
        var entity = new Example{Title = "test", Id = 1};
        var domainEvent = new ExampleCreatedEvent(entity);
        
        // Act
        entity.AddDomainEvent(domainEvent);

        // Assert
        Assert.Contains(domainEvent, entity.DomainEvents);
        Assert.Equal(1, entity.Id);
    }

    [Fact]
    public void RemoveDomainEvent_ShouldRemoveEventFromDomainEventsList()
    {
        // Arrange
        var entity = new Example{Title = "test"};
        var domainEvent = new ExampleCreatedEvent(entity);
        entity.AddDomainEvent(domainEvent);

        // Act
        entity.RemoveDomainEvent(domainEvent);

        // Assert
        Assert.DoesNotContain(domainEvent, entity.DomainEvents);
    }

    [Fact]
    public void ClearDomainEvents_ShouldClearDomainEventsList()
    {
        // Arrange
        var entity = new Example{Title = "test"};
        var domainEvent = new ExampleCreatedEvent(entity);
        entity.AddDomainEvent(domainEvent);
        entity.AddDomainEvent(domainEvent);

        // Act
        entity.ClearDomainEvents();

        // Assert
        Assert.Empty(entity.DomainEvents);
    }     
}