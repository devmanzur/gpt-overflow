using FluentAssertions;
using GPTOverflow.Core.UserManagement.Models;
using NetArchTest.Rules;
using Xunit;

namespace GPTOverflow.Core.Tests.Unit.UserManagement;

public class ArchitecturalIntegrityTests
{
    private readonly List<string> _externalModulesAndPackages = new List<string>
    {
        "GPTOverflow.Core.StackExchange",
        "GPTOverflow.API"
    };

    [Fact]
    public void AcceptanceTestShouldNotDependOnCoreModels()
    {
        var types = Types.InAssemblies(new[] { typeof(ApplicationUser).Assembly })
            .That()
            .ResideInNamespace("GPTOverflow.Core.UserManagement")
            .GetTypes();

        types.Should().NotBeEmpty();

        var result = Types.InCurrentDomain()
            .That()
            .ResideInNamespace("GPTOverflow.Core.UserManagement")
            .Should()
            .NotHaveDependencyOnAny(_externalModulesAndPackages.ToArray())
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}