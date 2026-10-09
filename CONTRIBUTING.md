# Contributing Guide

このプロジェクトへの貢献方法・開発ルールをまとめたファイルです。

---

## Development Flow

```
Issue 作成 → feature ブランチ → 実装（AI 支援）→ PR → コードレビュー → main
```

### Branch Strategy

| ブランチ | 用途 |
|---|---|
| `main` | リリース済みの安定版 |
| `feature/*` | 機能開発・バグ修正 |

### Commit Format

[Conventional Commits](https://www.conventionalcommits.org/) に従う。

| プレフィックス | 用途 |
|---|---|
| `feat` | 新機能 |
| `fix` | バグ修正 |
| `refactor` | リファクタリング（機能変更なし） |
| `docs` | ドキュメントのみの変更 |
| `chore` | ビルド・設定・依存関係の変更 |

- **WIP 禁止**: 動作しないコードはコミットしない

---

## Code Design Principles

- **変更範囲は必要最小限**（over-engineering しない、YAGNI 原則）
- **DRY の判断**: 2回の重複では抽象化しない、3回目で検討
- **既存コードの再利用**: 新規実装前に類似機能がないか確認する
- **既存パターンに従う**: 命名規則・ディレクトリ構造・アーキテクチャ
- **TODO/FIXME を残さない**: 実装するか Issue に記録する
- **`Core` は UI 非依存**: `System.Windows.Forms` / `System.Drawing` を参照しない

---

## Code Style

- メソッドは 50 行以内、単一責務
- コメントは「なぜそうするか」のみ書く（コードから自明な処理には書かない）
- デバッグ用の出力を本番コードに残さない
- 書式は `dotnet format`、規則の重大度は `.editorconfig` で管理する

---

## Naming Conventions

| 対象 | 規則 | 例 |
|---|---|---|
| 型・メソッド・プロパティ | PascalCase | `SettingsStore`, `Load` |
| ローカル変数・引数 | camelCase | `settingsPath` |
| private フィールド | `_camelCase` | `_notifyIcon` |
| 定数 | PascalCase | `AppName` |
| テストメソッド | `Method_Condition_Expected` | `Load_ReturnsDefaults_WhenFileIsMissing` |
| ブランチ | `feature/issue-{番号}-{概要}` | `feature/issue-42-add-auth` |

---

## Code Review Checklist

### PR Author

- [ ] lint とテストが通る（`make all`）
- [ ] 新機能にテストを追加した（`Core` のロジック）
- [ ] ドキュメントを必要に応じて更新した
- [ ] シークレット・ローカルパスがコードに含まれていない
- [ ] 警告を無効化した場合は、PR に理由を書いた
- [ ] AI が生成したコードをレビューしてからコミットした

### Reviewer

- [ ] ロジックが要件を満たしている
- [ ] テストカバレッジが十分
- [ ] 命名規則に従っている
- [ ] 不要な依存追加がない
- [ ] セキュリティリスクがない

---

## CI

```
changes → security（pre-commit）→ lint（dotnet format・警告をエラーにしたビルド）→ build（テスト・publish・MSI）→ gate
```

`gate`（ワークフロー名 `CI`）が Branch Protection の必須ステータスチェック。全て通過しないと PR はマージ不可。

| コマンド | 内容 |
|---|---|
| `make lint` | `dotnet format` と、警告をエラーにしたビルド |
| `make test` | xUnit のテスト実行 |
| `make msi` | publish して MSI をビルド |
| `make all` | lint + test を一括実行 |
