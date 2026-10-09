# Architecture

モジュール・コンポーネント構造。初回の実装後に実態に合わせて更新する。

## Overview

```text
MyApp.App  ──►  MyApp.Core
(UI・起動)       (ロジック・設定)

MyApp.Core.Tests ──► MyApp.Core
```

- 依存の向きは `App → Core`。`Core` は UI に依存しない（`net10.0`）。逆向き・循環は禁止
- `installer/` は `.slnx` に含めず、`dotnet publish` した `MyApp.App` のフォルダを入力にして MSI を作る

## MyApp.App

| コンポーネント | 役割 |
|---|---|
| `Program` | 起動処理。単一インスタンス（名前付き Mutex）、未処理例外のログ、`ApplicationConfiguration.Initialize()` |
| `TrayApplicationContext` | トレイ常駐の本体。`NotifyIcon` とメニュー（設定…・終了）を持つ。隠したフォームは置かない |
| `SettingsForm` | 独立した「設定」ウィンドウ。1 つだけ開く |
| `AppLog` | 未処理例外を `%APPDATA%\MyApp\app.log` に追記する最小のログ |

## MyApp.Core

| コンポーネント | 役割 |
|---|---|
| `AppSettings` | ユーザーごとの設定の型 |
| `SettingsStore` | `%APPDATA%\MyApp\settings.json` の読み書き。ファイルが無い・壊れている場合は既定値 |
| `Greeter` | 雛形のロジックの例。実際のロジックに置き換える |

## Data Flow

1. 起動 → `Program.Main` が Mutex を取得（取得できなければ終了）
2. `TrayApplicationContext` が `SettingsStore.Load` で設定を読み、ツールチップを更新する
3. 「設定…」→ `SettingsForm` で編集 → 保存で `SettingsStore.Save` → 閉じたらツールチップを更新する
