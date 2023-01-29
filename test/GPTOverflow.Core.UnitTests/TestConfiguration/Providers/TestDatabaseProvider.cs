using GPTOverflow.Core.CrossCuttingConcerns.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace GPTOverflow.Core.UnitTests.TestConfiguration.Providers;

public class TestDatabaseProvider<TContext> : IDisposable where TContext : DbContext
{
    private readonly SqliteConnection _connection;
    public IDbContextFactory<TContext> ContextFactory { get; init; }

    public TestDatabaseProvider()
    {
        _connection = new SqliteConnection("DataSource=test;mode=memory");
        _connection.Open();

        var dbContextOptions = new DbContextOptionsBuilder<TContext>()
            .UseSqlite(_connection)
            .Options;

        var httpContextMock = new Mock<IHttpContextAccessor>();
        var domainEventDispatcherMock = new Mock<IDomainEventsDispatcher>();
        var dbContextFactoryMock = new Mock<IDbContextFactory<TContext>>();
        dbContextFactoryMock
            .Setup(mock => mock.CreateDbContextAsync(new CancellationToken()).Result)
            .Returns(() =>
            {
                var db = (TContext)Activator.CreateInstance(typeof(TContext), dbContextOptions,
                    domainEventDispatcherMock.Object,
                    httpContextMock.Object)!;
                db.Database.EnsureCreated();
                return db;
            });
        ContextFactory = dbContextFactoryMock.Object;
    }

    public void Dispose()
    {
        _connection.Close();
        _connection.Dispose();
        GC.SuppressFinalize(this);
    }
}