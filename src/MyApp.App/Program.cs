namespace MyApp.App;

internal static class Program
{
    internal const string AppName = "MyApp";

    [STAThread]
    private static void Main()
    {
        // 単一インスタンス: 2 つ目の起動は何もせずに終了する
        using var mutex = new Mutex(true, $"{AppName}.SingleInstance", out var createdNew);
        if (!createdNew)
        {
            return;
        }

        Application.ThreadException += (_, e) => AppLog.WriteFatal(e.Exception);
        AppDomain.CurrentDomain.UnhandledException += (_, e) => AppLog.WriteFatal(e.ExceptionObject as Exception);
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

        ApplicationConfiguration.Initialize();
        Application.Run(new TrayApplicationContext());
    }
}
