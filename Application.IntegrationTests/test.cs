using Application.Examples.Queries.GetExamples;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Application.IntegrationTests;

public class test
{
    [Fact]
    public async void test1()
    {
        var factory = new CustomWebApplicationFactory();
        var scopeFactory = factory.Services.GetRequiredService<IServiceScopeFactory>();

        using var scope = scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var result = await mediator.Send(new GetExamplesQuery());
        
        Assert.NotNull(result);
    }
}