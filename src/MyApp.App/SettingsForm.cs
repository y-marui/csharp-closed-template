using MyApp.Core;

namespace MyApp.App;

/// <summary>
/// 独立した「設定」ウィンドウの骨格。設定項目を足すときは、<see cref="AppSettings"/> にプロパティを足し、
/// ここにコントロールを足す。
/// </summary>
internal sealed class SettingsForm : Form
{
    private readonly string _settingsPath;
    private readonly TextBox _displayNameBox = new() { Dock = DockStyle.Top };

    internal SettingsForm(string settingsPath)
    {
        _settingsPath = settingsPath;

        Text = "設定";
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ClientSize = new Size(360, 140);

        var settings = SettingsStore.Load(settingsPath);
        _displayNameBox.Text = settings.DisplayName;

        var label = new Label { Text = "表示名", Dock = DockStyle.Top, Padding = new Padding(0, 8, 0, 4), AutoSize = true };
        var save = new Button { Text = "保存", DialogResult = DialogResult.OK, Dock = DockStyle.Bottom };
        var cancel = new Button { Text = "キャンセル", DialogResult = DialogResult.Cancel, Dock = DockStyle.Bottom };
        save.Click += (_, _) => Save();

        Controls.Add(_displayNameBox);
        Controls.Add(label);
        Controls.Add(cancel);
        Controls.Add(save);
        AcceptButton = save;
        CancelButton = cancel;
    }

    private void Save()
    {
        SettingsStore.Save(_settingsPath, new AppSettings { DisplayName = _displayNameBox.Text.Trim() });
        Close();
    }
}
