# Hamelin.Runtimes.GitHubActions

[![Pull Request](https://github.com/hamelin-org/Hamelin.Runtimes.GitHubActions/actions/workflows/pr.yml/badge.svg)](https://github.com/hamelin-org/Hamelin.Runtimes.GitHubActions/actions/workflows/pr.yml) [![Release](https://github.com/hamelin-org/Hamelin.Runtimes.GitHubActions/actions/workflows/release.yml/badge.svg)](https://github.com/hamelin-org/Hamelin.Runtimes.GitHubActions/actions/workflows/release.yml)

This package adds integration for running Hamelin pipelines in a GitHub Actions environment.

## Installation

To install the package, you can use the following command:

```bash
dotnet add package Hamelin.Runtimes.GitHubActions
```

## Usage

### Registration

To add GitHub Actions runtime support to your Hamelin pipeline, you can use the `AddGitHubActionsRuntime` extension method on the `IServiceCollection`.

Runtime integration will only be fully registered if the `GitHubActionsRuntimeOptions.RuntimeDetector` detects that the application is running in a GitHub Actions environment. See [Runtime Detection](#runtime-detection) for more details.

```csharp
using Hamelin;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = PipelineApplication.CreateBuilder(args);

builder.Services
    .AddGitHubActionsRuntime();

// ...

var pipeline = builder.Build();

// ...

await pipeline.RunAsync();
```

### Options

You can configure the GitHub Actions runtime by providing options to the `AddGitHubActionsRuntime` method. The options allow you to customize the behavior of the runtime, such as the runtime detector and logging settings.s

### Runtime Detection

The runtime integration will only be fully registered if the `RuntimeDetector` delegate returns `true`. By default, this is done by checking for the presence of the `GITHUB_ACTIONS` environment variable. If you need to customize this behavior, you can provide your own `RuntimeDetector` implementation.

If the runtime isn't detected, you will still be able to inject `IGitHubActionsCommands` into your pipeline steps, but a stub implementation will be resolved instead.

If you need to check if the runtime is detected, you can inject the `IGitHubActionsContext` interface, which provides an `IsEnabled` property.

### Logging

The GitHub Actions integration includes a logging formatter that adapts the logging output for GitHub Actions. Warnings and errors are raised as notices on the pipeline, and debug logs are outputted using `::debug::` syntax which makes them respect GitHub Actions' debug setting.

### Commands

With the GitHub Actions integration registered, you can inject the `IGitHubActionsCommands` interface into your pipeline steps to run commands in the GitHub Actions environment, including manually writing logs, grouping log messages and setting outputs.

#### Log Messages

You can log messages directly to the GitHub Actions log using the `LogNotice`, `LogWarning` and `LogError` methods:

```csharp
public class MyPipelineStep(IGitHubActionsCommands gh) : IPipelineStep
{
    public Task Run(CancellationToken cancellationToken = new())
    {
        gh.LogError(
            message: "This is an error message.",
            title: "Notice",
            file: "README.md",
            startLine: 1,
            endLine: 1,
            startColumn: 1,
            endColumn: 1
        );
        return Task.CompletedTask;
    }
}
```

#### Log Grouping

Logs are grouped automatically by pipeline step. However, you can also create custom log groups using the `StartGroup` and `EndGroup` methods, or the `WithGroup` method. GitHub Actions logs don't support nested groups, so this functionality is mostly useful for creating groups outside of pipeline steps such as in hooks.

```csharp
public class MyPipelineStep(ILogger<MyPipelineStep> logger, GitHubActionsCommands gh) : IPipelineStep
{
    public Task Run(CancellationToken cancellationToken = new())
    {
        gh.BeginGroup("My Custom Group");
        logger.LogInformation("This log is inside the custom group.");
        gh.EndGroup();

        using (gh.WithGroup("My Other Custom Group"))
        {
            logger.LogInformation("This log is inside the other custom group.");
        }
        return Task.CompletedTask;
    }
}
```

#### Job Summaries

GitHub Actions supports job summaries that are displayed in the UI after a job completes. You can append content to the job summary using the `AppendJobSummary` method. Internally, this writes to a file specified by the `GITHUB_STEP_SUMMARY` environment variable, so you can call this method multiple times to append content.

```csharp
public class MyPipelineStep(ILogger<MyPipelineStep> logger, GitHubActionsCommands gh) : IPipelineStep
{
    public async Task Run(CancellationToken cancellationToken = new())
    {
        await gh.AppendJobSummary("# Job Summary\nThis is the summary of the job.");
    }
}
```
