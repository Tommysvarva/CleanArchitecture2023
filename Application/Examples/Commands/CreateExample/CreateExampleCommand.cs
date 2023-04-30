
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Examples.Commands.CreateExample;

public record CreateExampleCommand : IRequest<int>
{
    public string? Title { get; init; }
}

public class CreateExampleCommandHandler : IRequestHandler<CreateExampleCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateExampleCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateExampleCommand request, CancellationToken cancellationToken)
    {
        var entity = new Example
        {
            Title = request.Title
        };

        _context.Examples.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
