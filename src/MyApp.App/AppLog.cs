using System.Globalization;

namespace MyApp.App;

/// <summary>
/// 未処理例外を <c>%APPDATA%\MyApp\app.log</c> に追記する最小のログ（ロギングのライブラリは入れない）。
/// </summary>
internal static class AppLog
{
    private static readonly string FilePath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        Program.AppName,
        "app.log");

    internal static void WriteFatal(Exception? exception)
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);
            var timestamp = DateTimeOffset.Now.ToString("O", CultureInfo.InvariantCulture);
            File.AppendAllText(FilePath, $"{timestamp} {exception}{Environment.NewLine}");
        }
        catch (IOException)
        {
            // ログに書けなくても、アプリの終了処理を妨げない
        }
        catch (UnauthorizedAccessException)
        {
        }
    }
}
