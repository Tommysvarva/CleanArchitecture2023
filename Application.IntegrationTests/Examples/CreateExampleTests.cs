using System.Text;
using System.Text.Json;
using Domain.Entities;
using Xunit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;

namespace Application.IntegrationTests.ExamplesController;

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
    public async Task Create()
    {
        var example = new Example
        {
            Title = "test"
        };
        
        var response = await _factory.SendAsync(example, "/api/examples");
        var content = await response.Content.ReadAsStringAsync();
        var created =  JsonSerializer.Deserialize<JsonElement>(content);
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Get()
    {
        var response = await _httpClient.GetAsync("/api/examples");

        response.EnsureSuccessStatusCode();
    }


    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => _factory.ResetDatabaseAsync();
}