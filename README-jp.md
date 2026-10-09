# C# クローズド Template

> **このファイルは正本（日本語版）です。**
> 英語版（参照）は [README.md](README.md) を参照してください。

[![License: All Rights Reserved](https://img.shields.io/badge/License-All%20Rights%20Reserved-red.svg)](LICENSE)
[![CI](https://github.com/y-marui/csharp-closed-template/actions/workflows/ci.yml/badge.svg)](https://github.com/y-marui/csharp-closed-template/actions/workflows/ci.yml)
[![Charter Check](https://github.com/y-marui/csharp-closed-template/actions/workflows/dev-charter-check.yml/badge.svg)](https://github.com/y-marui/csharp-closed-template/actions/workflows/dev-charter-check.yml)

.NET 10 + Claude Code + GitHub Copilot で、Windows 向けのトレイ常駐アプリ（WinForms）を作るためのテンプレート。小規模チーム（1〜3人）向け。

---

## Meta Info

| 項目 | 内容 |
|---|---|
| 開発対象 | Windows デスクトップアプリ（WinForms、トレイ常駐） |
| 開発環境 | 小規模チーム（1〜3人） |
| 主言語 | 日本語 |
| ライセンス | All Rights Reserved |
| AI ツール | Claude Code / GitHub Copilot |
| 動作環境 | Windows 10 (1903+) / Windows 11、.NET 10 SDK |
| 配布 | インストーラー（MSI）に一本化 |

---

## Features

- ✅ **.NET 10 LTS**: `global.json` で SDK を固定し、ローカルと CI で同じビルドにする
- ✅ **実効性のある lint**: Roslyn アナライザ（`latest-Recommended`）+ `dotnet format`。CI では警告をエラーにする
- ✅ **Central Package Management**: NuGet のバージョンを `Directory.Packages.props` に集約
- ✅ **テスト**: xUnit v3（Microsoft.Testing.Platform）。ロジックは UI 非依存の `Core` に置く
- ✅ **トレイ常駐の土台**: 単一インスタンス・高 DPI・未処理例外のログ・`%APPDATA%` の JSON 設定・設定ウィンドウ
- ✅ **MSI に一本化**: WiX 6 で、publish したフォルダを MSI にする。タグの push で GitHub Releases に添付
- ✅ **AI ファースト**: Claude Code・GitHub Copilot との協働を前提とした構成
- ✅ **セキュリティ内包**: pre-commit フック（gitleaks 等）を同梱

---

## Requirements

| ツール | バージョン |
|---|---|
| .NET SDK | 10（`global.json` で固定） |
| pre-commit | 最新安定版 |
| make（任意） | `mingw32-make` 等。なくても `dotnet` コマンドを直接実行できる |

---

## Quick Start

### 1. Create a Repository from the Template

GitHub の **Use this template → Create a new repository** をクリックしてリポジトリを作成する。

### 2. Rename the Project

プレースホルダ `MyApp` を実際の名前に置き換える。

| 対象 | 内容 |
|---|---|
| フォルダ・ファイル | `src/MyApp.App`・`src/MyApp.Core`・`tests/MyApp.Core.Tests`・`installer/MyApp.Installer.wixproj`・`MyApp.slnx` |
| 名前空間・アセンブリ名 | `MyApp.App`・`MyApp.Core`（`.cs`・`.csproj`・`.slnx`） |
| `Program.AppName` | `%APPDATA%` のフォルダ名と Mutex 名に使う |
| `installer/Package.wxs` | `Name`・`Manufacturer`、**`UpgradeCode` を新しい GUID に**（`[guid]::NewGuid()`。以後変えない） |
| CI | `.github/workflows/ci.yml`・`release.yml` のソリューション名・パス |
| Makefile | `SLN` と `installer/` のパス |


### 3. Initial README Setup

```sh
# README_TEMPLATE-jp.md → README-jp.md にリネームして日本語正本に使う
mv README_TEMPLATE-jp.md README-jp.md
mv README_TEMPLATE.md README.md
# プレースホルダを実際の値に置き換える（{user}・{repo}・{workflow}・[USERNAME]・[BMC_USERNAME] など）
```

### 4. Build, Test, and Lint

```powershell
dotnet build MyApp.slnx
dotnet test --solution MyApp.slnx
dotnet format MyApp.slnx --verify-no-changes
```

### 5. Run and Build the Installer

```powershell
dotnet run --project src/MyApp.App
dotnet publish src/MyApp.App -c Release -o publish
dotnet build installer/MyApp.Installer.wixproj -c Release -p:PublishDir=$PWD\publish
```

### 6. Set Up Security Hooks

```sh
pre-commit install
pre-commit install --hook-type commit-msg
pre-commit run --all-files
```

---

## Project Structure

```text
.
├── global.json                    # SDK・テストランナー・WiX SDK のバージョン
├── Directory.Build.props          # 共通プロパティ（Version・解析設定）
├── Directory.Packages.props       # NuGet のバージョン（CPM）
├── .editorconfig
├── MyApp.slnx
├── src/
│   ├── MyApp.App/                 # WinForms トレイアプリ（UI）
│   └── MyApp.Core/                # UI 非依存のロジック・設定
├── tests/MyApp.Core.Tests/        # xUnit v3
├── installer/                     # WiX 6（MSI。.slnx には含めない）
└── docs/                          # architecture・file-map・specification・ui-design
```

詳細な方針は [docs/dev-charter/topics/csharp/CSHARP_DEV_ENV.md](docs/dev-charter/topics/csharp/CSHARP_DEV_ENV.md) を参照。

---

## License

All Rights Reserved — [LICENSE](LICENSE)

---
*この文書には英語版 [README.md](README.md) があります。編集時は同一コミットで更新してください。*
