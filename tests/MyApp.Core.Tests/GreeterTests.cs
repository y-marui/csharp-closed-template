using Xunit;

namespace MyApp.Core.Tests;

public class GreeterTests
{
    [Theory]
    [InlineData(null, "こんにちは")]
    [InlineData("", "こんにちは")]
    [InlineData("  ", "こんにちは")]
    [InlineData("ゆき", "こんにちは、ゆき")]
    public void Greet_ReturnsExpected(string? name, string expected) =>
        Assert.Equal(expected, Greeter.Greet(name));
}
