using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Examples.Queries.GetExamples;

public record GetExamplesQuery : IRequest<List<Example>>;

public class GetExamplesQueryHandler : IRequestHandler<GetExamplesQuery, List<Example>>
{
    private readonly IApplicationDbContext _context;

    public GetExamplesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Example>> Handle(GetExamplesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Examples
            .AsNoTracking()
            .OrderBy(t => t.Title)
            .ToListAsync(cancellationToken);
    }
}
