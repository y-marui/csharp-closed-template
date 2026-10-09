# File Map

ファイルレベルの依存関係マップ。初回探索後に追記していく運用。

## Repository Root

| ファイル | 役割 |
|---|---|
| `global.json` | SDK・テストランナー（Microsoft.Testing.Platform）・MSBuild SDK（WiX）のバージョン |
| `Directory.Build.props` | 全プロジェクト共通のプロパティ（`Version`・`Nullable`・解析設定・CI での警告エラー化） |
| `Directory.Packages.props` | NuGet のバージョン（Central Package Management） |
| `.editorconfig` | 整形・コードスタイル・アナライザの重大度（テストでは `CA1707` を緩める） |
| `MyApp.slnx` | ソリューション（`installer/` は含めない） |
| `Makefile` | `build`・`test`・`lint`・`run`・`publish`・`msi` |

## src/

```text
src/
├── MyApp.App/
│   ├── Program.cs                    # 起動・単一インスタンス・例外ログ
│   ├── TrayApplicationContext.cs     # トレイ・メニュー（Core に依存）
│   ├── SettingsForm.cs               # 設定ウィンドウ（Core に依存）
│   └── AppLog.cs                     # app.log への追記
└── MyApp.Core/
    ├── AppSettings.cs                # 設定の型
    ├── SettingsStore.cs              # JSON の読み書き
    └── Greeter.cs                    # 雛形のロジックの例
```

## tests/

| ファイル | 対象 |
|---|---|
| `tests/MyApp.Core.Tests/GreeterTests.cs` | `Greeter` |
| `tests/MyApp.Core.Tests/SettingsStoreTests.cs` | `SettingsStore`（往復・欠損・破損・null） |

## installer/

| ファイル | 役割 |
|---|---|
| `installer/MyApp.Installer.wixproj` | WiX 6 のプロジェクト。`Version` は `Directory.Build.props` から、`PublishDir` は引数で受ける |
| `installer/Package.wxs` | MSI の定義（perMachine・MajorUpgrade・publish フォルダを `Files` で取り込む・スタートメニューのショートカット） |

## .github/workflows/

| ファイル | 役割 |
|---|---|
| `ci.yml` | `changes`・`security`・`lint`・`build`・`gate`。runner は `LINUX_RUNNER` / `WINDOWS_RUNNER` で切り替え可能 |
| `release.yml` | タグ `vX.Y.Z` で MSI をビルドし、GitHub Releases に添付する |
| `dev-charter-check.yml` | dev-charter の更新を検知して更新 PR を作る |
| `auto-assign-self.yml` | 他者・bot の PR を assign する |
