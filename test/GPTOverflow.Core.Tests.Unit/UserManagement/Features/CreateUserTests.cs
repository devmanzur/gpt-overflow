using AutoFixture;
using AutoFixture.AutoMoq;
using Bogus;
using FluentAssertions;
using GPTOverflow.Core.CrossCuttingConcerns.Exceptions;
using GPTOverflow.Core.Tests.Unit._config.Providers;
using GPTOverflow.Core.UserManagement.Brokers.Persistence;
using GPTOverflow.Core.UserManagement.Features;
using GPTOverflow.Core.UserManagement.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GPTOverflow.Core.Tests.Unit.UserManagement.Features;

using static CreateUser;

public class CreateUserTests
{
    private static readonly Faker Faker = new();
    private readonly IFixture _fixture;

    public CreateUserTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization()
        {
            ConfigureMembers = true
        });
    }

    [Fact]
    public async Task Should_CreateNewUser_When_AllRequirementsAreSatisfied()
    {
        // Arrange
        var emailAddress = Faker.Person.Email!;
        var memberRole = new Role(UserRole.Member);
        using var testDatabaseProvider = new TestDatabaseProvider<UserManagementDbContext>();
        await using var dbContext = await testDatabaseProvider.ContextFactory.CreateDbContextAsync();
        // Clean db to make sure there is no such entry
        await dbContext.Users
            .Where(x => x.EmailAddress == emailAddress)
            .ExecuteDeleteAsync();
        dbContext.Roles.Add(memberRole);
        await dbContext.SaveChangesAsync();
        _fixture.Inject(dbContext);
        var command = new Command(emailAddress);
        var handler = _fixture.Create<Handler>();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Email.Should().Be(emailAddress);
        result.Value.Id.Should().NotBeNull();

        var savedUser = await dbContext.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(x => x.Id == new Guid(result.Value.Id!));
        savedUser.Should().NotBeNull();
        savedUser!.EmailAddress.Should().Be(emailAddress);
        savedUser.Role.Name.Should().Be(UserRole.Member);
    }

    [Fact]
    public async Task Should_ThrowValidationException_When_EmailAddressIsNotValid()
    {
        // Arrange
        var invalidEmailAddress = Faker.Random.String();
        var command = new Command(invalidEmailAddress);
        using var testDatabaseProvider = new TestDatabaseProvider<UserManagementDbContext>();
        await using var dbContext = await testDatabaseProvider.ContextFactory.CreateDbContextAsync();
        _fixture.Inject(dbContext);
        var handler = _fixture.Create<Handler>();

        // Act
        Func<Task> action = async () => { await handler.Handle(command, CancellationToken.None); };

        // Assert
        await action.Should().ThrowExactlyAsync<DomainValidationException>("Invalid email address");
    }

    [Fact]
    public async Task Should_ThrowValidationException_When_EmailAddressAlreadyExists()
    {
        // Arrange
        var emailAddress = Faker.Internet.Email();
        var command = new Command(emailAddress);
        var memberRole = new Role(UserRole.Member);
        using var testDatabaseProvider = new TestDatabaseProvider<UserManagementDbContext>();
        await using var dbContext = await testDatabaseProvider.ContextFactory.CreateDbContextAsync();
        dbContext.Roles.Add(memberRole);
        await dbContext.Users.AddAsync(new ApplicationUser(emailAddress)
        {
            RoleId = memberRole.Id
        });
        await dbContext.SaveChangesAsync();
        _fixture.Inject(dbContext);
        var handler = _fixture.Create<Handler>();

        // Act
        Func<Task> action = async () => { await handler.Handle(command, CancellationToken.None); };

        // Assert
        await action.Should().ThrowExactlyAsync<DomainValidationException>("Email address already exists");
    }

    [Fact]
    public async Task Should_ThrowCriticalSystemException_When_MemberRoleNotFound()
    {
        // Arrange
        var emailAddress = Faker.Internet.Email();
        var command = new Command(emailAddress);
        using var testDatabaseProvider = new TestDatabaseProvider<UserManagementDbContext>();
        await using var dbContext = await testDatabaseProvider.ContextFactory.CreateDbContextAsync();
        // Clean db to make sure there is no such entry
        await dbContext.Users
            .Where(x => x.EmailAddress == emailAddress)
            .ExecuteDeleteAsync();

        await dbContext.SaveChangesAsync();
        _fixture.Inject(dbContext);
        var handler = _fixture.Create<Handler>();

        // Act
        Func<Task> action = async () => { await handler.Handle(command, CancellationToken.None); };

        // Assert
        await action.Should().ThrowExactlyAsync<CriticalSystemException>("Required member role not found!");
    }
    
}