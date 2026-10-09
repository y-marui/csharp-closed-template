using MyApp.Core;

namespace MyApp.App;

/// <summary>
/// トレイ常駐の本体。隠したフォームを置かず、<see cref="ApplicationContext"/> に <see cref="NotifyIcon"/> を載せる。
/// </summary>
internal sealed class TrayApplicationContext : ApplicationContext
{
    private readonly NotifyIcon _notifyIcon;
    private readonly string _settingsPath = SettingsStore.DefaultPath(Program.AppName);
    private SettingsForm? _settingsForm;

    internal TrayApplicationContext()
    {
        var menu = new ContextMenuStrip();
        menu.Items.Add("設定…", null, (_, _) => ShowSettings());
        menu.Items.Add(new ToolStripSeparator());
        menu.Items.Add("終了", null, (_, _) => ExitThread());

        _notifyIcon = new NotifyIcon
        {
            Icon = SystemIcons.Application,
            ContextMenuStrip = menu,
            Visible = true,
        };
        _notifyIcon.DoubleClick += (_, _) => ShowSettings();

        RefreshTooltip();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _notifyIcon.Dispose();
            _settingsForm?.Dispose();
        }

        base.Dispose(disposing);
    }

    private void RefreshTooltip()
    {
        var settings = SettingsStore.Load(_settingsPath);
        _notifyIcon.Text = Greeter.Greet(settings.DisplayName);
    }

    private void ShowSettings()
    {
        // 設定ウィンドウは 1 つだけ開く
        if (_settingsForm is { IsDisposed: false })
        {
            _settingsForm.Activate();
            return;
        }

        _settingsForm = new SettingsForm(_settingsPath);
        _settingsForm.FormClosed += (_, _) => RefreshTooltip();
        _settingsForm.Show();
    }
}
