# C# Closed Template

> **This is the reference (English) version.**
> The canonical (Japanese) version is [README-jp.md](README-jp.md).

[![License: All Rights Reserved](https://img.shields.io/badge/License-All%20Rights%20Reserved-red.svg)](LICENSE)
[![CI](https://github.com/y-marui/csharp-closed-template/actions/workflows/ci.yml/badge.svg)](https://github.com/y-marui/csharp-closed-template/actions/workflows/ci.yml)
[![Charter Check](https://github.com/y-marui/csharp-closed-template/actions/workflows/dev-charter-check.yml/badge.svg)](https://github.com/y-marui/csharp-closed-template/actions/workflows/dev-charter-check.yml)

A template for building Windows tray-resident apps (WinForms) with .NET 10, Claude Code, and GitHub Copilot. For small teams (1–3 people).

---

## Meta Info

| Item | Details |
|---|---|
| Target | Windows desktop app (WinForms, tray-resident) |
| Team | Small team (1–3 people) |
| Primary language | Japanese |
| License | All Rights Reserved |
| AI tools | Claude Code / GitHub Copilot |
| Runtime | Windows 10 (1903+) / Windows 11, .NET 10 SDK |
| Distribution | Installer (MSI) only |

---

## Features

- ✅ **.NET 10 LTS**: the SDK is pinned in `global.json`, so local and CI builds match
- ✅ **A lint that actually checks**: Roslyn analyzers (`latest-Recommended`) plus `dotnet format`; warnings are errors on CI
- ✅ **Central Package Management**: NuGet versions live in `Directory.Packages.props`
- ✅ **Tests**: xUnit v3 (Microsoft.Testing.Platform); logic lives in the UI-free `Core`
- ✅ **Tray app foundation**: single instance, high DPI, unhandled-exception log, JSON settings under `%APPDATA%`, a settings window
- ✅ **MSI only**: WiX 6 packages the published folder; pushing a tag attaches the MSI to a GitHub Release
- ✅ **AI-first**: layout assumes collaboration with Claude Code and GitHub Copilot
- ✅ **Security built in**: pre-commit hooks (gitleaks and more) included

---

## Requirements

| Tool | Version |
|---|---|
| .NET SDK | 10 (pinned in `global.json`) |
| pre-commit | latest stable |
| make (optional) | e.g. `mingw32-make`; you can run the `dotnet` commands directly instead |

---

## Quick Start

### 1. Create a Repository from the Template

Click **Use this template → Create a new repository** on GitHub.

### 2. Rename the Project

Replace the `MyApp` placeholder with the real name.

| Target | What to change |
|---|---|
| Folders and files | `src/MyApp.App`, `src/MyApp.Core`, `tests/MyApp.Core.Tests`, `installer/MyApp.Installer.wixproj`, `MyApp.slnx` |
| Namespaces and assembly names | `MyApp.App`, `MyApp.Core` (`.cs`, `.csproj`, `.slnx`) |
| `Program.AppName` | used for the `%APPDATA%` folder and the mutex name |
| `installer/Package.wxs` | `Name`, `Manufacturer`, and **a new `UpgradeCode` GUID** (`[guid]::NewGuid()`; never change it afterwards) |
| CI | the solution name and paths in `.github/workflows/ci.yml` and `release.yml` |
| Makefile | `SLN` and the `installer/` path |

### 3. Initial README Setup

```sh
# Rename README_TEMPLATE-jp.md to README-jp.md (canonical, Japanese) and README_TEMPLATE.md to README.md
mv README_TEMPLATE-jp.md README-jp.md
mv README_TEMPLATE.md README.md
# Replace the placeholders ({user}, {repo}, {workflow}, [USERNAME], [BMC_USERNAME], ...)
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
├── global.json                    # SDK, test runner, WiX SDK versions
├── Directory.Build.props          # shared properties (Version, analysis settings)
├── Directory.Packages.props       # NuGet versions (CPM)
├── .editorconfig
├── MyApp.slnx
├── src/
│   ├── MyApp.App/                 # WinForms tray app (UI)
│   └── MyApp.Core/                # UI-free logic and settings
├── tests/MyApp.Core.Tests/        # xUnit v3
├── installer/                     # WiX 6 (MSI; not part of the .slnx)
└── docs/                          # architecture, file-map, specification, ui-design
```

See [docs/dev-charter/topics/csharp/CSHARP_DEV_ENV.md](docs/dev-charter/topics/csharp/CSHARP_DEV_ENV.md) for the full policy.

---

## License

All Rights Reserved — [LICENSE](LICENSE)

---
*This document has a Japanese canonical version [README-jp.md](README-jp.md). Update both in the same commit when editing.*
