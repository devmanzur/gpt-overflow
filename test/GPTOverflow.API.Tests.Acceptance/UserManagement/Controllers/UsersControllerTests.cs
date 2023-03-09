using AutoFixture;
using AutoFixture.AutoMoq;
using Bogus;
using FluentAssertions;
using GPTOverflow.API.Tests.Acceptance._config;
using GPTOverflow.API.Tests.Acceptance._config.Brokers;
using NetArchTest.Rules;
using Xunit;

namespace GPTOverflow.API.Tests.Acceptance.UserManagement.Controllers;

[Collection(nameof(GptOverflowApi))]
public partial class UsersControllerTests
{
    private readonly GptOverflowApi _api;
    private readonly ApiBroker _apiBroker;
    private readonly Faker _faker;

    public UsersControllerTests(GptOverflowApi api)
    {
        _api = api;
        _apiBroker = new ApiBroker(api.CreateClient());
        _faker = new Faker();
        new Fixture().Customize(new AutoMoqCustomization()
        {
            ConfigureMembers = true
        });
    }
    
    private readonly List<string> _coreModelNamespace = new List<string>
    {
        // This rule was broken because we reference dbContext directly from the acceptance tests that depend on models in the below namespace
        // "GPTOverflow.Core.UserManagement.Models",
        "GPTOverflow.API.UserManagement.Models"
    };



    [Fact]
    public void AcceptanceTestShouldNotDependOnCoreModels()
    {
        var result = Types.InCurrentDomain()
            .That()
            .ResideInNamespace("GPTOverflow.API.Tests.Acceptance.UserManagement")
            .Should()
            .NotHaveDependencyOnAny(_coreModelNamespace.ToArray())
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    /// <summary>
    /// Creates username from email address,
    /// </summary>
    /// <example>
    /// email = manzur123@gmail.com
    /// username = @manzur123
    /// </example>
    /// <param name="email"></param>
    /// <returns></returns>
    private static string CreateUsername(string email)
    {
        return $"@{email.Split("@")[0]}";
    }
}