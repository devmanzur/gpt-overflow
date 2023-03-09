using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace GPTOverflow.API.Tests.Acceptance._config.Attributes;

public class AutoSubstituteDependencyAttribute : AutoDataAttribute
{
    public AutoSubstituteDependencyAttribute() : base(() =>
    {
        var fixture = new Fixture();
        fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });
        return fixture;
    })
    {
    }
}