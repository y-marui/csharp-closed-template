namespace MyApp.Core;

/// <summary>
/// 雛形のロジックの例（UI に依存しない純粋な関数）。実際のロジックに置き換える。
/// </summary>
public static class Greeter
{
    public static string Greet(string? name) =>
        string.IsNullOrWhiteSpace(name) ? "こんにちは" : $"こんにちは、{name}";
}
