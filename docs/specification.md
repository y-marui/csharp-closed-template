# Specification

機能仕様・データフロー。テンプレートから作ったプロジェクトの仕様に置き換える。

## App Behavior (Template)

- 起動するとタスクトレイにアイコンが出る。ウィンドウは開かない
- 二重起動はできない（2 つ目は何もせずに終了する）
- トレイのアイコンをダブルクリック、またはメニューの「設定…」で設定ウィンドウを開く（1 つだけ）
- メニューの「終了」でアプリを終了する
- 未処理例外は `%APPDATA%\MyApp\app.log` に追記して終了する

## Settings

| 項目 | 保存先 | 内容 |
|---|---|---|
| `DisplayName` | `%APPDATA%\MyApp\settings.json` | トレイのツールチップの表示名。空なら既定の文言 |

設定ファイルが無い・壊れている場合は、既定値で起動する（起動を妨げない）。

## Distribution

- 配布は MSI のみ（`installer/`）。perMachine（`Program Files`）にインストールし、スタートメニューにショートカットを作る
- 上書きアップグレードできる（`MajorUpgrade`）。`UpgradeCode` は変更しない
- バージョンは `Directory.Build.props` の `<Version>`。リリースはタグ `vX.Y.Z` の push
