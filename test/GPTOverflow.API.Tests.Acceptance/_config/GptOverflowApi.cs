using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using GPTOverflow.Core.StackExchange.Brokers.Persistence;
using GPTOverflow.Core.UserManagement.Brokers.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace GPTOverflow.API.Tests.Acceptance._config;

public class GptOverflowApi : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly TestcontainerDatabase _dbContainer;
    
    public GptOverflowApi()
    {
        _dbContainer = new TestcontainersBuilder<MsSqlTestcontainer>()
            .WithDatabase(new MsSqlTestcontainerConfiguration()
            {
                Database = "test_db",
                Password = "SwIN12345678",
            })
            .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveDbContext<UserManagementDbContext>();

            services.AddDbContext<UserManagementDbContext>(options =>
            {
                options.UseSqlServer(
                        $"{_dbContainer.ConnectionString};Integrated Security=True;TrustServerCertificate=True;Trusted_Connection=False")
                    .UseSnakeCaseNamingConvention();
            });
            
            services.RemoveDbContext<StackExchangeDbContext>();

            services.AddDbContext<StackExchangeDbContext>(options =>
            {
                options.UseSqlServer(
                        $"{_dbContainer.ConnectionString};Integrated Security=True;TrustServerCertificate=True;Trusted_Connection=False")
                    .UseSnakeCaseNamingConvention();
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Test";
                options.DefaultChallengeScheme = "Test";
            });
            
            services.AddTransient<IAuthenticationSchemeProvider, MockSchemeProvider>();

        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }
}