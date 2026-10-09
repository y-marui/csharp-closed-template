# {プロジェクト名}

> **このファイルは正本（日本語版）です。**
> 英語版（参照）は [README.md](README.md) を参照してください。

[![License: All Rights Reserved](https://img.shields.io/badge/License-All%20Rights%20Reserved-red.svg)](LICENSE)
[![CI](https://github.com/{user}/{repo}/actions/workflows/{workflow}.yml/badge.svg)](https://github.com/{user}/{repo}/actions/workflows/{workflow}.yml)
[![Charter Check](https://github.com/{user}/{repo}/actions/workflows/dev-charter-check.yml/badge.svg)](https://github.com/{user}/{repo}/actions/workflows/dev-charter-check.yml)
[![GitHub Sponsors](https://img.shields.io/github/sponsors/[USERNAME]?style=social)](https://github.com/sponsors/[USERNAME])
[![Buy Me a Coffee](https://img.shields.io/badge/Buy%20Me%20a%20Coffee-donate-yellow.svg)](https://www.buymeacoffee.com/[BMC_USERNAME])

{一行概要：「何を・誰のために・どう解決するか」を 1 文で}

---

## Install

[Releases](https://github.com/{user}/{repo}/releases) から最新の `{プロジェクト名}-vX.Y.Z-win-x64.msi` をダウンロードして実行する。
アンインストールは「アプリと機能」から行う。

---

## Development

.NET 10 SDK が必要（`global.json` で固定）。

```powershell
git clone https://github.com/{user}/{repo}.git
cd {repo}
dotnet build {ソリューション名}.slnx
```

| コマンド | 内容 |
|---|---|
| `make run` | アプリを起動する |
| `make test` | xUnit のテストを実行する |
| `make lint` | `dotnet format` と、警告をエラーにしたビルド |
| `make msi` | publish して MSI を `installer/bin/x64/Release/` にビルドする |
| `make all` | lint + test を一括実行 |

---

## License

All Rights Reserved — [LICENSE](LICENSE)

---
*この文書には英語版 [README.md](README.md) があります。編集時は同一コミットで更新してください。*
