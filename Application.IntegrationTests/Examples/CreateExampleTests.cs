using System.Text.Json;
using Domain.Entities;
using Xunit;

namespace Application.IntegrationTests.Examples;

[Collection("Test collection")]
public class CreateExampleControllerTests : IAsyncLifetime
{
    private readonly HttpClient _httpClient;
    private readonly CustomWebApplicationFactory _factory;

    public CreateExampleControllerTests(CustomWebApplicationFactory apiFactory)
    {
        _factory = apiFactory;
        _httpClient = apiFactory.HttpClient;
    }

    [Fact]
    public async Task Create_ShouldCreate_Example()
    {
        var example = new Example
        {
            Title = "example"
        };
        
        var response = await _factory.HttpClientSendAsync(example, "/api/examples");
        var exampleId = JsonSerializer.Deserialize<int>(await response.Content.ReadAsStreamAsync());
        var createdExample = await _factory.DbFindAsync<Example>(exampleId);
        
        response.EnsureSuccessStatusCode();
        Assert.IsType<int>(exampleId);
        Assert.NotNull(createdExample);
        Assert.Equal("example", createdExample.Title);
    }

    [Fact]
    public async Task Create_ShouldThrown_ValidationException()
    {
        var example = new Example
        {
            Title = "e"
        };
        
    }


    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => _factory.ResetDatabaseAsync();
}