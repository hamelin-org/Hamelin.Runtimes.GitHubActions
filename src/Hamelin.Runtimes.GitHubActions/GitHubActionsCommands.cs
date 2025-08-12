namespace Hamelin.Runtimes.GitHubActions;

/// <inheritdoc />
/// <remarks>
/// Based on https://docs.github.com/en/actions/reference/workflows-and-actions/workflow-commands
/// </remarks>
public class GitHubActionsCommands : IGitHubActionsCommands
{
    /// <inheritdoc />
    public void LogDebug(string message) => WriteCommand("debug", message, null);

    /// <inheritdoc />
    public void LogNotice(
        string message,
        string? title = null,
        string? file = null,
        int? startLine = null,
        int? endLine = null,
        int? startColumn = null,
        int? endColumn = null
    ) => WriteFileCommand("notice", message, title, file, startLine, endLine, startColumn, endColumn);

    /// <inheritdoc />
    public void LogWarning(
        string message,
        string? title = null,
        string? file = null,
        int? startLine = null,
        int? endLine = null,
        int? startColumn = null,
        int? endColumn = null
    ) => WriteFileCommand("warning", message, title, file, startLine, endLine, startColumn, endColumn);

    /// <inheritdoc />
    public void LogError(
        string message,
        string? title = null,
        string? file = null,
        int? startLine = null,
        int? endLine = null,
        int? startColumn = null,
        int? endColumn = null
    ) => WriteFileCommand("error", message, title, file, startLine, endLine, startColumn, endColumn);

    /// <inheritdoc />
    public void BeginGroup(string title)
    {
        WriteCommand("group", title, null);
    }

    /// <inheritdoc />
    public void EndGroup()
    {
        WriteCommand("endgroup", "", null);
    }

    /// <inheritdoc />
    public void SetJobSummary(string summary)
    {
        // The discrepancy between "job summary" and "step summary" is as per GitHub's documentation.
        Environment.SetEnvironmentVariable("GITHUB_STEP_SUMMARY", summary);
    }

    private static void WriteFileCommand(string command, string message, string? title = null, string? file = null, int? startLine = null, int? endLine = null, int? startColumn = null, int? endColumn = null)
    {
        var args = new Dictionary<string, string?>()
        {
            { "title", title },
            { "file", file },
            { "line", startLine?.ToString() },
            { "endLine", endLine?.ToString() },
            { "col", startColumn?.ToString() },
            { "endColumn", endColumn?.ToString() }
        };
        WriteCommand(command, message, args);
    }

    private static void WriteCommand(string command, string message, Dictionary<string, string?>? args)
    {
        string argString = "";
        if (args != null)
        {
            string[] existingArgs = args
                .Where(kvp => !string.IsNullOrEmpty(kvp.Value))
                .Select(kvp => $"{kvp.Key}={kvp.Value}")
                .ToArray();
            if (existingArgs.Length > 0)
            {
                argString = " " + string.Join(",", existingArgs);
            }
        }
        Console.Out.WriteLine($"::{command}{argString}::{message}");
    }
}
