# AI_CONTEXT.md

このファイルは Claude Code・GitHub Copilot など AI ツールがセッション開始時に参照する唯一のコンテキストファイルです。

## Document Reference Order

AI はセッション開始時に以下の順序でドキュメントを参照する：

1. **AI_CONTEXT.md**（本ファイル：AI固有の指示・guardrails）
2. **README-jp.md**（概要・セットアップ）
3. **DEVELOPING.md**（ビルド・実装規約）— 詳細が必要な場合のみ
4. **docs/architecture.md**（モジュール・コンポーネント構造）— 詳細が必要な場合のみ
5. **docs/file-map.md**（ファイルレベルの依存関係）— 詳細が必要な場合のみ
6. **docs/specification.md**（機能仕様・データフロー）— 詳細が必要な場合のみ

---

## Project Overview

**目的**: AI支援開発用のクローズドな Windows デスクトップアプリ（WinForms、トレイ常駐）のテンプレート。
.NET 10 + Claude Code + GitHub Copilot 前提の開発体制。

**チーム規模**: 1〜3名の小規模チームで開発。将来的に外部メンテナンスへの移行を想定。

**言語ポリシー**: クローズドプロジェクトのため、ドキュメント・コメントは**日本語**を正本とする。

### Tech Stack

.NET SDK のバージョン管理・lint・プロジェクト構成・テスト・MSI・CI の一般方針は
[docs/dev-charter/topics/csharp/CSHARP_DEV_ENV.md](docs/dev-charter/topics/csharp/CSHARP_DEV_ENV.md)
を参照（ここには重複して書かない）。CI（GitHub Actions）は push / PR のたびに実行する。

### Main Directories

```
.
├── global.json / Directory.Build.props / Directory.Packages.props / .editorconfig
├── MyApp.slnx
├── src/
│   ├── MyApp.App/    # WinForms トレイアプリ（UI）
│   └── MyApp.Core/   # UI 非依存のロジック・設定
├── tests/MyApp.Core.Tests/   # xUnit v3
├── installer/        # WiX 6（MSI。.slnx には含めない）
├── docs/
│   └── dev-charter/  # 開発憲章（参照元）
└── .github/workflows/  # ci.yml・release.yml
```

### Module Structure

| モジュール | 役割 |
|---|---|
| `MyApp.App` | トレイ・設定ウィンドウ・起動処理（単一インスタンス・例外ログ） |
| `MyApp.Core` | 設定の型と保存・業務ロジック（UI に依存しない） |

依存の向きは `App → Core`。`Core` が `App`（UI）を参照することは禁止。

---

## Coding Rules

- 可読性優先
- メソッドは50行以内
- 単一責務
- 循環依存禁止
- コメントは「なぜそうするか」のみ書く（コードから自明な処理には書かない）
- 警告を無効化する場合は、PR に理由を書く（`.editorconfig` で重大度を管理する）
- サードパーティの NuGet を追加する場合は、**ユーザーに確認する**（憲章の依存方針）

---

## Document Sync Rule

仕様・ルール・構成に変更が生じたとき、変更と同じ作業内で関連ドキュメントを更新する。
対象は `docs/` 内のファイルに限らず、`AI_CONTEXT.md`・`README-jp.md`・`README.md` 等のルートファイルも含む。

---

## dev-charter Modification Rules

`docs/dev-charter/` 配下のファイルは**直接編集しない**。

- 変更が必要な場合は dev-charter リポジトリ本体に Issue を立て、`git subtree pull` でアップデートを取り込む
- `git subtree pull` によるアップデートのみ許可する
- プロジェクト固有のルールは `AI_CONTEXT.md` または専用ファイルに記載する

---

## Project-Specific Rules

- バージョンは `Directory.Build.props` の `<Version>` を単一の情報源とする（`CHANGELOG.md` と揃える）
- 配布は MSI のみ。exe を直接配る経路や、アプリ自身がショートカットを作る経路は追加しない
- `installer/Package.wxs` の `UpgradeCode` は製品ごとに固定の GUID。**変更しない**
- 設定は `%APPDATA%\<App>\settings.json`。実行機固有の値は `*.example` だけをコミットする

---

## Template Usage

テンプレートから新規プロジェクトを作る際に、以下を実施する：

1. `MyApp` を実際の名前に置き換える（フォルダ・ファイル・名前空間・`Program.AppName`・CI・Makefile。手順は README-jp.md の Quick Start）
2. `installer/Package.wxs` の `UpgradeCode` を新しい GUID にし、`Name`・`Manufacturer` を設定する
3. `LICENSE` の `[YEAR]` / `[AUTHOR]`、README / FUNDING の `{user}` / `{repo}` / `{workflow}` / `[USERNAME]` / `[BMC_USERNAME]` を実値に置き換える
4. `pre-commit install` と `pre-commit install --hook-type commit-msg` を実行する
5. `.github/workflows/dev-charter-check.yml` を確認する（dev-charter の更新 PR を自動で受ける）
6. private リポジトリで self-hosted runner を使う場合は、runner を登録してから `LINUX_RUNNER` / `WINDOWS_RUNNER` を設定する

---

## AI Tool Assignments

- **使用ツール**：Claude Code、GitHub Copilot
- **標準担当の正本**：`docs/dev-charter/AI_COLLABORATION_RULES.md` の「AI Tool Responsibilities」と「Rules for Multi-AI Usage」
- **プロジェクト固有の上書き**：なし

---

## Prohibited Actions

- シークレット・認証情報のコミット
- ローカル絶対パスのハードコード
- `UpgradeCode` の変更
- `Core` から UI（`System.Windows.Forms` / `System.Drawing`）への依存の追加
- ビルド成果物（`bin/`・`obj/`・`publish/`・`*.msi`）のコミット
