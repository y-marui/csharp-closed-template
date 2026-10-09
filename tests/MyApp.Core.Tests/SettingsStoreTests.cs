using Xunit;

namespace MyApp.Core.Tests;

public sealed class SettingsStoreTests : IDisposable
{
    private readonly string _directory = Path.Combine(Path.GetTempPath(), "MyApp.Tests." + Guid.NewGuid().ToString("N"));

    public void Dispose()
    {
        if (Directory.Exists(_directory))
        {
            Directory.Delete(_directory, recursive: true);
        }
    }

    [Fact]
    public void Load_ReturnsDefaults_WhenFileIsMissing()
    {
        var settings = SettingsStore.Load(Path.Combine(_directory, "settings.json"));

        Assert.Equal(string.Empty, settings.DisplayName);
    }

    [Fact]
    public void Save_ThenLoad_RoundTrips()
    {
        var path = Path.Combine(_directory, "nested", "settings.json");

        SettingsStore.Save(path, new AppSettings { DisplayName = "ゆき" });

        Assert.Equal("ゆき", SettingsStore.Load(path).DisplayName);
    }

    [Fact]
    public void Load_ReturnsDefaults_WhenFileIsCorrupt()
    {
        Directory.CreateDirectory(_directory);
        var path = Path.Combine(_directory, "settings.json");
        File.WriteAllText(path, "{ not json");

        Assert.Equal(string.Empty, SettingsStore.Load(path).DisplayName);
    }

    [Fact]
    public void Save_Throws_WhenSettingsIsNull() =>
        Assert.Throws<ArgumentNullException>(() => SettingsStore.Save(Path.Combine(_directory, "x.json"), null!));
}
