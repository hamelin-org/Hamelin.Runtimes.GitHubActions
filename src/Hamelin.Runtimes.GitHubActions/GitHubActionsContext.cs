namespace Hamelin.Runtimes.GitHubActions;

internal class GitHubActionsContext : IGitHubActionsContext
{
    public bool IsEnabled { get; init; }
}
