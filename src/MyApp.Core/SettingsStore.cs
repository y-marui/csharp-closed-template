using System.Text.Json;

namespace MyApp.Core;

/// <summary>
/// <see cref="AppSettings"/> の JSON での読み書き。ファイルが無い・壊れている場合は既定値を返す
/// （設定の不備でアプリが起動できなくならないようにする）。
/// </summary>
public static class SettingsStore
{
    private static readonly JsonSerializerOptions Options = new() { WriteIndented = true };

    /// <summary>既定の保存先 <c>%APPDATA%\&lt;appName&gt;\settings.json</c>。</summary>
    public static string DefaultPath(string appName) =>
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), appName, "settings.json");

    public static AppSettings Load(string path)
    {
        if (!File.Exists(path))
        {
            return new AppSettings();
        }

        try
        {
            return JsonSerializer.Deserialize<AppSettings>(File.ReadAllText(path), Options) ?? new AppSettings();
        }
        catch (JsonException)
        {
            return new AppSettings();
        }
    }

    public static void Save(string path, AppSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        var directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        File.WriteAllText(path, JsonSerializer.Serialize(settings, Options) + "\n");
    }
}
