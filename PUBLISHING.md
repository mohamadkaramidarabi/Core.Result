# Publishing Core.Result

Step-by-step guide to build, test, push to GitHub, and publish to NuGet.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [GitHub CLI](https://cli.github.com/) (`gh`)
- A [NuGet.org](https://www.nuget.org/) account with an API key

## 1. Authenticate

### GitHub

```powershell
gh auth login
```

Follow the prompts and sign in to the account that owns the repository.

### NuGet

1. Open [nuget.org/account/apikeys](https://www.nuget.org/account/apikeys)
2. Create an API key with **Push** scope for `Core.Result`
3. Store the key securely (it is shown only once)

Optional — save the key for future pushes:

```powershell
dotnet nuget push "bin\Release\Core.Result.1.0.0.nupkg" `
  --api-key YOUR_NUGET_API_KEY `
  --source https://api.nuget.org/v3/index.json

# Or store it for the nuget.org source:
dotnet nuget locals all --clear
# Then use --api-key on each push, or set NUGET_API_KEY in your environment
```

## 2. Run tests

From the repository root:

```powershell
dotnet test Core.Result.Test\Core.Result.Test.csproj -c Release
```

All tests must pass before publishing.

## 3. Bump the version

Update the `<Version>` element in `Core.Result.csproj`:

```xml
<Version>1.0.1</Version>
```

Follow [semantic versioning](https://semver.org/):

- **Patch** — bug fixes, no API changes
- **Minor** — new features, backward compatible
- **Major** — breaking API changes

Commit the version change:

```powershell
git add Core.Result.csproj
git commit -m "Bump version to 1.0.1"
```

## 4. Create the Release package

```powershell
dotnet pack Core.Result.csproj -c Release
```

Output files:

| File | Purpose |
|------|---------|
| `bin\Release\Core.Result.{version}.nupkg` | NuGet package |
| `bin\Release\Core.Result.{version}.snupkg` | Symbol package (debugging) |

## 5. Push to GitHub

### First-time setup

If the remote repository does not exist yet:

```powershell
gh repo create Core.Result --public --source=. --remote=origin --push
```

### Subsequent releases

```powershell
git push origin main
```

Optional — tag the release:

```powershell
git tag v1.0.1
git push origin v1.0.1
```

## 6. Publish to NuGet

```powershell
dotnet nuget push "bin\Release\Core.Result.1.0.0.nupkg" `
  --api-key YOUR_NUGET_API_KEY `
  --source https://api.nuget.org/v3/index.json `
  --skip-duplicate
```

Push the symbol package as well:

```powershell
dotnet nuget push "bin\Release\Core.Result.1.0.0.snupkg" `
  --api-key YOUR_NUGET_API_KEY `
  --source https://api.nuget.org/v3/index.json `
  --skip-duplicate
```

Replace `1.0.0` with the version you are publishing.

## 7. Verify

- **NuGet:** [nuget.org/packages/Core.Result](https://www.nuget.org/packages/Core.Result)
- **GitHub:** [github.com/mohamadkaramidarabi/Core.Result](https://github.com/mohamadkaramidarabi/Core.Result)

Install and smoke-test:

```powershell
dotnet add package Core.Result --version 1.0.0
```

## Full release checklist

- [ ] Tests pass (`dotnet test -c Release`)
- [ ] Version bumped in `Core.Result.csproj`
- [ ] Changes committed and pushed to GitHub
- [ ] Release package built (`dotnet pack -c Release`)
- [ ] `.nupkg` pushed to NuGet
- [ ] `.snupkg` pushed to NuGet
- [ ] Package visible on nuget.org
- [ ] Git tag created (optional)

## Troubleshooting

| Problem | Solution |
|---------|----------|
| `401 Unauthorized` on NuGet push | Provide a valid API key with Push permission |
| `409 Conflict` on NuGet push | Version already exists — bump `<Version>` and rebuild |
| `Repository not found` on git push | Run `gh auth login`, then `gh repo create` |
| Package not found after push | NuGet indexing can take a few minutes |
