namespace Hamelin.Runtimes.GitHubActions;

/// <summary>
/// Provides an interface for checking if the current runtime is GitHub Actions.
/// </summary>
public interface IGitHubActionsContext
{
    /// <summary>
    /// True if the current runtime is GitHub Actions; otherwise, false.
    /// </summary>
    bool IsEnabled { get; }
}
