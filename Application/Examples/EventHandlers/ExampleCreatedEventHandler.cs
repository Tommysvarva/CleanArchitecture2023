using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Examples.EventHandlers;

public class ExampleCreatedEventHandler : INotificationHandler<ExampleCreatedEvent>
{
    private readonly ILogger<ExampleCreatedEventHandler> _logger;

    public ExampleCreatedEventHandler(ILogger<ExampleCreatedEventHandler> logger)
    {
        _logger = logger;
    }
    
    public Task Handle(ExampleCreatedEvent notification, CancellationToken cancellationToken)
    {
       _logger.LogInformation("Domain Event published: {DomainEvent}", notification.GetType().Name);
       
       return Task.CompletedTask;
    }
}