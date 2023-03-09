using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace GPTOverflow.Core.Tests.Unit._config;

public class AutoMoqDataAttribute : AutoDataAttribute
{
    public AutoMoqDataAttribute()
        : base(() =>
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization() { });
            return fixture;
        })
    {
    }
}