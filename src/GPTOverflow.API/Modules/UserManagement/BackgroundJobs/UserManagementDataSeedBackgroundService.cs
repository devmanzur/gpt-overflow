using GPTOverflow.Core.UserManagement.Brokers.Persistence;
using GPTOverflow.Core.UserManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace GPTOverflow.API.Modules.UserManagement.BackgroundJobs;

public class UserManagementDataSeedBackgroundService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;


    public UserManagementDataSeedBackgroundService(IServiceProvider serviceProvider)
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
        var context = scope.ServiceProvider.GetRequiredService<UserManagementDbContext>();
        await ApplyPendingMigrationsAsync(context, cancellationToken);
        await SeedRolesAsync(context, cancellationToken);
        await SeedRolePermissionsAsync(context, cancellationToken);
    }

    private async Task SeedRolePermissionsAsync(UserManagementDbContext context, CancellationToken cancellationToken)
    {
        return;
    }

    private async Task SeedRolesAsync(UserManagementDbContext context, CancellationToken cancellationToken)
    {
        var memberRoleExists = await context.Roles.AsNoTracking()
            .AnyAsync(x => x.Name == UserRole.Member, cancellationToken: cancellationToken);
        if (!memberRoleExists)
        {
            context.Roles.Add(new Role(UserRole.Member){});
        }
        var moderatorRoleExists = await context.Roles.AsNoTracking()
            .AnyAsync(x => x.Name == UserRole.Moderator, cancellationToken: cancellationToken);
        if (!moderatorRoleExists)
        {
            context.Roles.Add(new Role(UserRole.Moderator){});
        }
        await context.SaveChangesAsync(cancellationToken);
    }

    private static async Task ApplyPendingMigrationsAsync(UserManagementDbContext context,
        CancellationToken cancellationToken)
    {
        if (context.Database.IsSqlServer())
        {
            await context.Database.MigrateAsync(cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}