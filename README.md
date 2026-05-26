# KitchenChaos

Unity C# 项目。

## 环境准备

### 1. 安装 .NET SDK

```bash
# macOS
brew install dotnet-sdk

# Windows
winget install Microsoft.DotNet.SDK.10

# 或从官网下载: https://dotnet.microsoft.com/download
```

### 2. 安装 Cocogitto（commit 信息校验）

```bash
cargo install cocogitto
```

### 3. 恢复 .NET 本地工具

```bash
dotnet tool restore
```

### 4. 安装 Git Hooks

```bash
dotnet husky install
```

> `dotnet husky install` 只需要在 clone 之后跑一次。

## 开始开发

1. 用 Unity Hub 打开项目（自动生成本地 `.slnx` 和 `.csproj`）
2. 用 VS Code 打开项目，安装推荐的扩展
3. 写代码，提交时自动触发检查：

| 时机 | 检查内容 |
|------|----------|
| `git commit` | `dotnet format`（格式化）+ `dotnet build`（Roslyn 分析） |
| commit 信息 | `cog verify`（Conventional Commits 格式校验） |
| IDE 保存 | 自动格式化 + 整理 using |
| IDE 编码 | Roslyn 全量实时分析 |

## Commit 规范

```bash
# 推荐：交互式创建 conventional commit
cog commit

# 或手动按格式提交
git commit -m "feat: add player controller"
git commit -m "fix: correct collision detection"
git commit -m "chore: update dependencies"
```

格式：`type(scope): description`

## 生成 Changelog

```bash
cog changelog
```
