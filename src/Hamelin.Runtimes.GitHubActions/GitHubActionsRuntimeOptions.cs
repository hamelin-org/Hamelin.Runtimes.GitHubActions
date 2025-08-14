namespace Hamelin.Runtimes.GitHubActions;

/// <summary>
/// Options for configuring the GitHub Actions runtime integration.
/// </summary>
public class GitHubActionsRuntimeOptions
{
    /// <summary>
    /// Determines whether log messages should be formatted for GitHub Actions.
    /// This will surface warnings and errors in a way that GitHub Actions recognizes, allowing them to be displayed prominently in the Actions UI.
    /// </summary>
    public bool EnableLogFormatting { get; set; } = true;

    /// <summary>
    /// The function that detects if the current runtime is GitHub Actions.
    /// </summary>
    /// <remarks>
    /// By default, this checks for the presence of the GITHUB_ACTIONS environment variable.s
    /// See https://docs.github.com/en/actions/reference/workflows-and-actions/variables for more details.
    /// </remarks>
    public Func<bool> RuntimeDetector { get; set; } = () => !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("GITHUB_ACTIONS"));
}
