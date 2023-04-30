using Application.Common.Interfaces;
using Application.Common.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Examples.Queries.GetExamples;

public record GetExamplesQuery : IRequest<List<ExampleDto>>;

public class GetExamplesQueryHandler : IRequestHandler<GetExamplesQuery, List<ExampleDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ExampleMapper _mapper = new();

    public GetExamplesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ExampleDto>> Handle(GetExamplesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.Examples
            .AsNoTracking()
            .OrderBy(t => t.Title)
            .ToListAsync(cancellationToken);
        
        return _mapper.CreateDto(entities);
    }
}