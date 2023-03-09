using GPTOverflow.Core.StackExchange.Brokers.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GPTOverflow.API.Modules.StackExchange.BackgroundJobs;

public class StackExchangeDataSeedBackgroundService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;


    public StackExchangeDataSeedBackgroundService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        await SeedUserDatabaseAsync(scope, cancellationToken);
    }

    private async Task SeedUserDatabaseAsync(IServiceScope scope,CancellationToken cancellationToken)
    {
        var context = scope.ServiceProvider.GetRequiredService<StackExchangeDbContext>();
        await ApplyPendingMigrationsAsync(context, cancellationToken);
    }

    private static async Task ApplyPendingMigrationsAsync(StackExchangeDbContext context,
        CancellationToken cancellationToken)
    {
        if (context.Database.IsSqlServer())
        {
            await context.Database.MigrateAsync(cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}