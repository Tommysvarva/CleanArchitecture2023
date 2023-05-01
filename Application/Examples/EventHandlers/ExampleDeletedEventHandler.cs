using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Examples.EventHandlers;

public class ExampleDeletedEventHandler : INotificationHandler<ExampleDeletedEvent>
{
    private readonly ILogger<ExampleDeletedEventHandler> _logger;

    public ExampleDeletedEventHandler(ILogger<ExampleDeletedEventHandler> logger)
    {
        _logger = logger;
    }
    
    public Task Handle(ExampleDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event published: {DomainEvent}", notification.GetType().Name);
       
        return Task.CompletedTask;
    }
}