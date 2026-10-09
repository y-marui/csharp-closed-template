# UI Design

UI 設計・コンポーネント仕様。テンプレートから作ったプロジェクトの UI に置き換える。

## Tray

| 要素 | 内容 |
|---|---|
| アイコン | 既定は `SystemIcons.Application`。プロジェクトのアイコンに置き換える |
| ツールチップ | 設定の表示名（空なら既定の文言） |
| ダブルクリック | 設定ウィンドウを開く |
| メニュー | 「設定…」「終了」 |

## Settings Window

- 独立したウィンドウ（`FormBorderStyle.FixedDialog`、画面中央）。1 つだけ開く
- 項目: 表示名（テキスト）
- ボタン: 「保存」（JSON に保存して閉じる）、「キャンセル」
- 設定項目を足すときは、`AppSettings` にプロパティを足し、`SettingsForm` にコントロールを足す

## High DPI

`PerMonitorV2`（`MyApp.App.csproj` の `ApplicationHighDpiMode`）。固定サイズのレイアウトは、DPI が変わると
崩れやすいため、コントロールは `Dock` やレイアウトパネルを使う。
