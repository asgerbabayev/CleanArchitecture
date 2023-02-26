using CleanArchitecture.Domain.Common;
using CleanArchitecture.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Respawn;

namespace CleanArchitecture.Application.IntegrationTests;

[TestFixture]
public abstract class BaseTestFixture
{
    private static Respawner _checkPoint = null;
    private static IConfiguration _configuration = null;
    private static IServiceScopeFactory _scopeFactory = null;
    private static WebApplicationFactory<Program> _factory = null;



    [OneTimeSetUp]
    public void RunBeforeAnyTest()
    {
        _factory = new CustomWebApplicationFactory();
        _configuration = _factory.Services.GetRequiredService<IConfiguration>();
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();

        _checkPoint = Respawner.CreateAsync(_configuration.GetConnectionString("test")!, new RespawnerOptions
        {
            TablesToIgnore = new Respawn.Graph.Table[] { "__EFMigrationHistory" }
        })
            .GetAwaiter()
            .GetResult();
    }

    public static async Task ResetState()
    {
        try
        {
            await _checkPoint.ResetAsync(_configuration.GetConnectionString("test")!);
        }
        catch (Exception) { }
    }

    [SetUp]
    public async Task TestSetup()
    {
        await Testing.ResetState();
    }


    public static async Task AddAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Add(entity);

        await context.SaveChangesAsync();
    }

    public static async Task<TResponse> SendAsycn<TResponse>(IRequest<TResponse> rquest)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
        return await mediator.Send(rquest);
    }

    public static async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues) where TEntity : BaseEntity
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        return await context.FindAsync<TEntity>(keyValues);
    } 

}
