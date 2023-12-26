using System.Data.Common;
using Infrastructure.Data;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
namespace Application.IntegrationTests;

public abstract class BaseIntegrationTest : IClassFixture<CustomWebApplicationFactory>
{
    protected readonly ApplicationDbContext DbContext;
    protected readonly ISender Sender;
    protected BaseIntegrationTest(CustomWebApplicationFactory factory)
    {
        var scope = factory.Services.CreateScope();
        Sender = scope.ServiceProvider.GetRequiredService<ISender>();
        DbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }
}