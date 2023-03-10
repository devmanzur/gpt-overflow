using AutoFixture;
using AutoFixture.AutoMoq;
using Bogus;
using CSharpFunctionalExtensions;
using FluentAssertions;
using GPTOverflow.Core.CrossCuttingConcerns.Exceptions;
using GPTOverflow.Core.Questionnaire.Features;
using GPTOverflow.Core.Questionnaire.Models;
using GPTOverflow.Core.Questionnaire.Persistence;
using GPTOverflow.Core.UnitTests.TestConfiguration.Providers;
using Xunit;

namespace GPTOverflow.Core.UnitTests.Questionnaire.Features;

using static PostNewQuestion;

public class PostNewQuestionTests
{
    private static readonly Faker Faker = new();
    private readonly IFixture _fixture;

    public PostNewQuestionTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization()
        {
            ConfigureMembers = true
        });
    }

    [Fact]
    public async Task Should_CompleteSuccessfully_When_AllRequirementsAreSatisfied()
    {
        // Arrange
        var mockedTags = new List<Tag>
        {
            new Tag { Name = Faker.Random.Word(), Description = Faker.Random.Word() },
            new Tag { Name = Faker.Random.Word(), Description = Faker.Random.Word() }
        };
        var account = new Account($"@{Faker.Person.Email.Split("@")[0]}");
        using var testDatabaseProvider = new TestDatabaseProvider<QuestionnaireDbContext>();
        await using var dbContext = await testDatabaseProvider.ContextFactory.CreateDbContextAsync();
        dbContext.Accounts.Add(account);
        dbContext.Tags.AddRange(mockedTags);
        await dbContext.SaveChangesAsync();
        _fixture.Inject(dbContext);
        var accountId = account.Id;
        var title = Faker.Random.String();
        var description = Faker.Random.String();
        var tags = mockedTags.Select(x => x.Name).ToList();
        var command = new Command(accountId, title, description, tags);
        var expectedResult = Result.Success(new CommandResponse(Faker.Random.String(), title, description));
        var handler = _fixture.Create<Handler>();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Title.Should().Be(expectedResult.Value.Title);
        result.Value.Id.Should().NotBeNull();
        result.Value.Description.Should().Be(expectedResult.Value.Description);

        var savedQuestion = dbContext.Questions.FirstOrDefault(x =>
            x.Id == new Guid(result.Value.Id!));
        savedQuestion.Should().NotBeNull();
        savedQuestion!.Title.Should().Be(command.Title);
        savedQuestion!.Description.Should().Be(command.Description);
    }

    [Fact]
    public async Task Should_ThrowValidationException_When_CommandIsNull()
    {
        // Arrange
        Command? command = null;
        var handler = _fixture.Create<Handler>();

        // Act
        Func<Task> action = async () => { await handler.Handle(command, CancellationToken.None); };

        // Assert
        await action.Should().ThrowExactlyAsync<DomainValidationException>("Command is null");
    }

    [Theory]
    [MemberData(nameof(GetCommandParameters))]
    public async Task Should_ThrowValidationException_When_CommandArgumentsAreInvalid(Guid accountId, string title,
        string description,
        string[]? tags)

    {
        // Arrange
        var command = new Command(accountId, title, description,
            tags == null ? new List<string>() : new List<string>(tags));
        using var testDatabaseProvider = new TestDatabaseProvider<QuestionnaireDbContext>();
        await using var dbContext = await testDatabaseProvider.ContextFactory.CreateDbContextAsync();
        _fixture.Inject(dbContext);
        var handler = _fixture.Create<Handler>();

        // Act
        Func<Task> action = async () => { await handler.Handle(command, CancellationToken.None); };

        // Assert
        await action.Should().ThrowExactlyAsync<DomainValidationException>("One or more Arguments are invalid");
    }

    public static IEnumerable<object[]> GetCommandParameters()
    {
        yield return new object[] { null, null, null, null };
        yield return new object[] { null, null, null, new string[] { "Tag1", "Tag2" } };
        yield return new object[] { null, null, "Description", new string[] { "Tag1", "Tag2" } };
        yield return new object[] { null, "Title", "Description", new string[] { "Tag1", "Tag2" } };
        yield return new object[] { Guid.NewGuid(), null, "Description", new string[] { "Tag1", "Tag2" } };
        yield return new object[] { Guid.NewGuid(), "", "Description", new string[] { "Tag1", "Tag2" } };
        yield return new object[] { Guid.NewGuid(), "Title", null, new string[] { "Tag1", "Tag2" } };
        yield return new object[] { Guid.NewGuid(), "Title", "", new string[] { "Tag1", "Tag2" } };
        yield return new object[]
            { Guid.NewGuid(), Faker.Random.String(2001), "Description", new string[] { "Tag1", "Tag2" } };
        yield return new object[] { Guid.NewGuid(), Faker.Random.String(2001), "Description", null };
    }


    [Fact]
    public async Task Should_ThrowValidationException_When_UserDoesNotExist()
    {
        // Arrange
        var mockedTags = new List<Tag>
        {
            new Tag { Name = Faker.Random.Word(), Description = Faker.Random.Word() },
            new Tag { Name = Faker.Random.Word(), Description = Faker.Random.Word() }
        };
        using var testDatabaseProvider = new TestDatabaseProvider<QuestionnaireDbContext>();
        await using var dbContext = await testDatabaseProvider.ContextFactory.CreateDbContextAsync();
        dbContext.Tags.AddRange(mockedTags);
        await dbContext.SaveChangesAsync();
        _fixture.Inject(dbContext);
        var accountId = Faker.Random.Guid();
        var title = Faker.Random.String();
        var description = Faker.Random.String();
        var tags = mockedTags.Select(x => x.Name).ToList();
        var command = new Command(accountId, title, description, tags);
        var handler = _fixture.Create<Handler>();

        // Act
        Func<Task> action = async () => { await handler.Handle(command, CancellationToken.None); };

        // Assert
        await action.Should().ThrowExactlyAsync<DomainValidationException>("Account doest not exist");
    }

    [Fact]
    public async Task Should_ReturnFailure_When_TagsDoNotExist()
    {
        // Arrange
        var mockedTags = new List<Tag>
        {
            new Tag { Name = Faker.Random.Word(), Description = Faker.Random.Word() },
            new Tag { Name = Faker.Random.Word(), Description = Faker.Random.Word() }
        };
        var account = new Account($"@{Faker.Person.Email.Split("@")[0]}");
        using var testDatabaseProvider = new TestDatabaseProvider<QuestionnaireDbContext>();
        await using var dbContext = await testDatabaseProvider.ContextFactory.CreateDbContextAsync();
        dbContext.Accounts.Add(account);
        await dbContext.SaveChangesAsync();
        _fixture.Inject(dbContext);
        var accountId = account.Id;
        var title = Faker.Random.String();
        var description = Faker.Random.String();
        var tags = mockedTags.Select(x => x.Name).ToList();
        var command = new Command(accountId, title, description, tags);
        var expectedResult = Result.Failure("Invalid tags");
        var handler = _fixture.Create<Handler>();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(expectedResult.Error);
    }
}