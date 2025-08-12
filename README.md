# Hamelin.Runtimes.GitHubActions

This package adds integration for running Hamelin pipelines in a GitHub Actions environment.

## Installation

To install the package, you can use the following command:

```bash
dotnet add package Hamelin.Runtimes.GitHubActions
```

## Usage

### Registration

To add GitHub Actions runtime support to your Hamelin pipeline, you can use the `AddGitHubActionsRuntime` extension method on the `IServiceCollection`.

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

### Logging

The GitHub Actions integration includes a logging provider that surfaces errors and warnings in the GitHub Actions UI.

### Commands

With the GitHub Actions integration registered, you can inject the `IGitHubActionsCommands` interface into your pipeline steps to run commands in the GitHub Actions environment, including manually writing logs, grouping log messages and setting outputs.



