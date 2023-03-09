using FluentAssertions;
using GPTOverflow.Core.UserManagement.Models;
using NetArchTest.Rules;
using Xunit;

namespace GPTOverflow.Core.Tests.Unit.StackExchange;

public class ArchitecturalIntegrityTests
{
    private readonly List<string> _externalModulesAndPackages = new List<string>
    {
        "GPTOverflow.Core.UserManagement",
        "GPTOverflow.API"
    };

    [Fact]
    public void AcceptanceTestShouldNotDependOnCoreModels()
    {
        var types = Types.InAssemblies(new[] { typeof(ApplicationUser).Assembly })
            .That()
            .ResideInNamespace("GPTOverflow.Core.StackExchange")
            .GetTypes();

        types.Should().NotBeEmpty();

        var result = Types.InCurrentDomain()
            .That()
            .ResideInNamespace("GPTOverflow.Core.StackExchange")
            .Should()
            .NotHaveDependencyOnAny(_externalModulesAndPackages.ToArray())
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}