namespace Hamelin.Runtimes.GitHubActions.Build.Services;

public interface ICommandRunner
{
    Task Run(string command, string[] arguments, CancellationToken cancellationToken);
}
