using Hamelin.Runtimes.GitHubActions.Build.Models;
using Hamelin.Runtimes.GitHubActions.Build.Services;
using Microsoft.Extensions.Options;

namespace Hamelin.Runtimes.GitHubActions.Build.Steps;

public class TestStep(IOptions<BuildOptions> options, ICommandRunner commands) : IPipelineStep
{
    public async Task Run(CancellationToken cancellationToken = default)
    {
        await commands.Run(
            command: "dotnet",
            arguments: ["test", "--no-build", "--configuration", options.Value.Configuration],
            cancellationToken
        );
    }
}
