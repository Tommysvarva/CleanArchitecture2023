using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest: notnull
{
    private readonly ILogger _logger;

    public LoggingBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }
    
    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Processed request: {Name} {@Request}",
            request.GetType().Name, request);

        return Task.CompletedTask;
    }
}