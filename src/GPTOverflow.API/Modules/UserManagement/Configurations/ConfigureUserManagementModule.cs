using GPTOverflow.Core.UserManagement.Brokers.Persistence;
using GPTOverflow.Core.UserManagement.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GPTOverflow.API.Modules.UserManagement.Configurations;

public static class ConfigureUserManagementModule
{
    public static void UserManagementModule(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<UserManagementDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("UserDatabase")!)
                .UseSnakeCaseNamingConvention();
        });
    }
}