# Developing

## Requirements

- Windows 10 (1903+) / Windows 11
- .NET 10 SDK（`global.json` で固定。`rollForward: latestFeature`）
- pre-commit

## Setup

```powershell
dotnet build MyApp.slnx
pre-commit install
pre-commit install --hook-type commit-msg
```

## Running Directly

```powershell
dotnet run --project src/MyApp.App
```

トレイにアイコンが出る。右クリックメニューの「設定…」で設定ウィンドウを開く。設定は
`%APPDATA%\MyApp\settings.json`、未処理例外のログは `%APPDATA%\MyApp\app.log`。

## Test

```powershell
dotnet test --solution MyApp.slnx
```

xUnit v3 のテストは Microsoft.Testing.Platform で動く（`global.json` の `test.runner`）。`--nologo` 等の
VSTest 時代のオプションは「不明なオプション」になる。Visual Studio のテストエクスプローラーが固まる場合は、
`dotnet test` で通ることを正とする。

## Lint

```powershell
dotnet format MyApp.slnx --verify-no-changes
$env:CI = "true"; dotnet build MyApp.slnx --no-incremental
```

CI と同じく、`CI=true` のときは警告がエラーになる（`Directory.Build.props`）。ローカルの通常のビルドでは
警告として表示される。

## Installer (MSI)

インストーラーは `.slnx` に含めず、publish のあとに単独でビルドする（publish したフォルダが入力のため）。

```powershell
dotnet publish src/MyApp.App -c Release -o publish
dotnet build installer/MyApp.Installer.wixproj -c Release -p:PublishDir=$PWD\publish
```

出力は `installer/bin/x64/Release/MyApp-v<Version>-win-x64.msi`。

- WiX のバージョンは `global.json` の `msbuild-sdks` で固定している
- self-hosted の Windows runner（標準ユーザー）では、MSI の検証（ICE）が `WIX0217` で失敗する。CI は `WINDOWS_RUNNER` が設定されているときだけ `-p:SuppressValidation=true` を付けて検証を省く（hosted とローカルでは検証する）。release で `gh` を使うため、`gh` もシステム全体の PATH に入れる
- WiX v6 以降は Open Source Maintenance Fee（OSMF）の対象。年間売上が 1 万ドルを超える組織は条件を確認する
- リリースは、タグ `vX.Y.Z` を push する（`.github/workflows/release.yml` が MSI を GitHub Releases に添付する）

## GitHub Settings

新しいリポジトリを作ったら、憲章の [`GITHUB_SETTINGS`](https://github.com/y-marui/dev-charter/blob/full/topics/GITHUB_SETTINGS.md) の手順で、リポジトリの設定を適用する（ファイルではなく、GitHub 側の設定なので、テンプレートからは引き継がれない）。特に次を確認する。

- `main-protection` Ruleset（有効、PR 必須、必須チェックは `CI` と `Dev Charter`。`Check / check` を直接登録しない）
- Settings → Actions → General: 「Allow GitHub Actions to create and approve pull requests」を ON（`check-charter.yml` が更新 PR を作る）
- 許可する Actions を `selected` にし、使う非 `actions/*` だけを許可する（`dorny/paths-filter@v4*`・`pre-commit/action@v3*`・`y-marui/dev-charter/.github/workflows/check-charter.yml@main`）。新しい非 `actions/*` を足すときは、push する前に許可リストへ追加する（先に push すると `startup_failure`）
- マージ後のブランチ自動削除、Auto-merge の許可、Wiki・Projects を ON
- private リポジトリで self-hosted runner を使う場合は、runner を登録してから `LINUX_RUNNER`・`WINDOWS_RUNNER` を設定する

## Conventions

- **Naming:** PascalCase for types/methods/properties, camelCase for locals/parameters, `_camelCase` for private fields
- **Comments:** Explain *why*, not *what* — see `docs/dev-charter/CODE_STYLE.md`
- **Commit messages:** Conventional Commits format (`feat:`, `fix:`, `docs:`, `chore:`)
- **Branching:** One branch per feature/fix; merge to `main` via PR
- **依存関係:** サードパーティの NuGet は既定で追加しない（追加はユーザーに確認し、理由を記録する）

## Architecture

See [docs/architecture.md](docs/architecture.md).
