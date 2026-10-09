namespace MyApp.Core;

/// <summary>
/// ユーザーごとの設定。<c>%APPDATA%\MyApp\settings.json</c> に保存する（<see cref="SettingsStore"/>）。
/// </summary>
public sealed class AppSettings
{
    /// <summary>トレイのツールチップなどに表示する名前。空ならアプリ名を使う。</summary>
    public string DisplayName { get; set; } = string.Empty;
}
