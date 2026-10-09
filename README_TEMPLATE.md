# {Project Name}

> **This file is the English reference.**
> The canonical version (Japanese) is [README-jp.md](README-jp.md).

[![License: All Rights Reserved](https://img.shields.io/badge/License-All%20Rights%20Reserved-red.svg)](LICENSE)
[![CI](https://github.com/{user}/{repo}/actions/workflows/{workflow}.yml/badge.svg)](https://github.com/{user}/{repo}/actions/workflows/{workflow}.yml)
[![Charter Check](https://github.com/{user}/{repo}/actions/workflows/dev-charter-check.yml/badge.svg)](https://github.com/{user}/{repo}/actions/workflows/dev-charter-check.yml)
[![GitHub Sponsors](https://img.shields.io/github/sponsors/[USERNAME]?style=social)](https://github.com/sponsors/[USERNAME])
[![Buy Me a Coffee](https://img.shields.io/badge/Buy%20Me%20a%20Coffee-donate-yellow.svg)](https://www.buymeacoffee.com/[BMC_USERNAME])

{One-line description: what it does, for whom, and how it helps.}

---

## Install

Download the latest `{Project Name}-vX.Y.Z-win-x64.msi` from [Releases](https://github.com/{user}/{repo}/releases) and run it.
Uninstall from "Apps & features".

---

## Development

Requires the .NET 10 SDK (pinned in `global.json`).

```powershell
git clone https://github.com/{user}/{repo}.git
cd {repo}
dotnet build {Solution}.slnx
```

| Command | Description |
|---|---|
| `make run` | Run the app |
| `make test` | Run the xUnit tests |
| `make lint` | `dotnet format` plus a build with warnings as errors |
| `make msi` | Publish, then build the MSI into `installer/bin/x64/Release/` |
| `make all` | Run lint + test |

---

## License

All Rights Reserved — [LICENSE](LICENSE)

---
*This document has a Japanese canonical version [README-jp.md](README-jp.md). Update both in the same commit when editing.*
