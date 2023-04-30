using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Example> Examples { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}