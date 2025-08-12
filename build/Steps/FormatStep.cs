using Hamelin.Runtimes.GitHubActions.Build.Services;

namespace Hamelin.Runtimes.GitHubActions.Build.Steps;

public class FormatStep(ICommandRunner commands) : IPipelineStep
{
    public async Task Run(CancellationToken cancellationToken = default)
    {
        await commands.Run(
            command: "dotnet",
            arguments: ["format", "--verify-no-changes"],
            cancellationToken
        );
    }
}
