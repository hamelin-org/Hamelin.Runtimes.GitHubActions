using Hamelin.Runtimes.GitHubActions.Build.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Hamelin.Runtimes.GitHubActions.Build.Steps;

public class CleanStep(ILogger<CleanStep> logger, IOptions<BuildOptions> options, IPipelineContext context) : IPipelineStep
{
    public Task Run(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Cleaning temp and artifact directories.");
        var cd = context.FileSystem.CurrentDirectory;
        cd.GetDirectory(options.Value.ArtifactsDirectory).Delete();
        cd.GetDirectory(options.Value.TempDirectory).Delete();
        return Task.CompletedTask;
    }
}
