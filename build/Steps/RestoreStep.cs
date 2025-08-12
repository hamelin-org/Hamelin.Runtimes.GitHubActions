using Hamelin.Runtimes.GitHubActions.Build.Services;

namespace Hamelin.Runtimes.GitHubActions.Build.Steps;

public class RestoreStep(ICommandRunner commands) : IPipelineStep
{
    public async Task Run(CancellationToken cancellationToken = default)
    {
        await commands.Run(
            command: "dotnet",
            arguments: ["restore"],
            cancellationToken
        );
    }
}
