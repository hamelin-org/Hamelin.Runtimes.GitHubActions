using Hamelin.Runtimes.GitHubActions.Build.Models;
using Hamelin.Runtimes.GitHubActions.Build.Services;
using Microsoft.Extensions.Options;

namespace Hamelin.Runtimes.GitHubActions.Build.Steps;

public class PackStep(IOptions<BuildOptions> options, ICommandRunner commands) : IPipelineStep
{
    public async Task Run(CancellationToken cancellationToken = default)
    {
        await commands.Run(
            command: "dotnet",
            arguments: [
                "pack",
                "--no-build",
                "--configuration", options.Value.Configuration,
                "--output", options.Value.ArtifactsDirectory
            ],
            cancellationToken
        );
    }
}
