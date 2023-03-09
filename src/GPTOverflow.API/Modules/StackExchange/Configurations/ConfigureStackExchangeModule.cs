using GPTOverflow.Core.StackExchange.Brokers.Persistence;
using GPTOverflow.Core.StackExchange.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GPTOverflow.API.Modules.StackExchange.Configurations;

public static class ConfigureStackExchangeModule
{
    public static void AddStackExchangeModule(this IServiceCollection services,ConfigurationManager configuration)
    {
        services.AddDbContext<StackExchangeDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("StackExchangeDatabase")!)
                .UseSnakeCaseNamingConvention();
        });
    }
}