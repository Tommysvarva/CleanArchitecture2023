using System.Data.Common;
using System.Text;
using API;
using Application.Common.Interfaces;
using Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using Respawn;
using Xunit;
using Xunit.Sdk;

namespace Application.IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    public HttpClient HttpClient { get; private set; } = default!;

    private Respawner _respawner = default!;
    private static IConfiguration _configuration = default!;
    private string _baseUri = default!;
    private static IServiceScopeFactory _scopeFactory = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configurationBuilder =>
        {
            var integrationConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Test.json")
                .AddEnvironmentVariables()
                .Build();

            configurationBuilder.AddConfiguration(integrationConfig);
        });

        builder.ConfigureServices((builder, services) =>
        {
            services.RemoveAll(typeof(IApplicationDbContext));
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        });
    }

    public async Task InitializeAsync()
    {
        _configuration = Services.GetRequiredService<IConfiguration>();
        _scopeFactory = Services.GetRequiredService<IServiceScopeFactory>();
        _baseUri = _configuration["BaseUri"]!.TrimEnd('/');
        await InitializeRespawner();
        HttpClient = CreateClient();
    }

    public new Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task<HttpResponseMessage> HttpClientSendAsync<TBody>(TBody body, string path)
    {
        var uri = $"{_baseUri}/{path.TrimStart('/')}";
        var jsonBody = JsonConvert.SerializeObject(body);
        using var request = new HttpRequestMessage(HttpMethod.Post, uri)
        {
            Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
        };

        return await HttpClient.SendAsync(request);
    }

    public async Task<TEntity?> DbFindAsync<TEntity>(params object[] keyValues)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        return await context.FindAsync<TEntity>(keyValues);
    }


    public async Task ResetDatabaseAsync()
    {
        await _respawner
            .ResetAsync(_configuration.GetConnectionString("DefaultConnection")!);
    }

    private async Task InitializeRespawner()
    {
        _respawner = await Respawner.CreateAsync(
            _configuration.GetConnectionString("DefaultConnection")!, new RespawnerOptions
            {
                DbAdapter = DbAdapter.SqlServer
            });
    }
}