namespace Hamelin.Runtimes.GitHubActions;

/// <summary>
/// Provides methods to interact with GitHub Actions commands.
/// </summary>
public interface IGitHubActionsCommands
{
    /// <summary>
    /// Logs a debug message to the GitHub Actions log.
    /// </summary>
    /// <param name="message">The debug message to log.</param>
    void LogDebug(string message);

    /// <summary>
    /// Logs a notice message to the GitHub Actions log.
    /// </summary>
    /// <param name="message">The notice message to log.</param>
    /// <param name="title">An optional title for the notice.</param>
    /// <param name="file">An optional file path associated with the notice.</param>
    /// <param name="startLine">An optional start line number in the file.</param>
    /// <param name="endLine">An optional end line number in the file.</param>
    /// <param name="startColumn">An optional start column number in the file.</param>
    /// <param name="endColumn">An optional end column number in the file.</param>
    void LogNotice(
        string message,
        string? title = null,
        string? file = null,
        int? startLine = null,
        int? endLine = null,
        int? startColumn = null,
        int? endColumn = null
    );

    /// <summary>
    /// Logs a warning message to the GitHub Actions log.
    /// </summary>
    /// <param name="message">The warning message to log.</param>
    /// <param name="title">An optional title for the warning.</param>
    /// <param name="file">An optional file path associated with the warning.</param>
    /// <param name="startLine">An optional start line number in the file.</param>
    /// <param name="endLine">An optional end line number in the file.</param>
    /// <param name="startColumn">An optional start column number in the file.</param>
    /// <param name="endColumn">An optional end column number in the file.</param>
    void LogWarning(
        string message,
        string? title = null,
        string? file = null,
        int? startLine = null,
        int? endLine = null,
        int? startColumn = null,
        int? endColumn = null
    );

    /// <summary>
    /// Logs an error message to the GitHub Actions log.
    /// </summary>
    /// <param name="message">The error message to log.</param>
    /// <param name="title">An optional title for the error.</param>
    /// <param name="file">An optional file path associated with the error.</param>
    /// <param name="startLine">An optional start line number in the file.</param>
    /// <param name="endLine">An optional end line number in the file.</param>
    /// <param name="startColumn">An optional start column number in the file.</param>
    /// <param name="endColumn">An optional end column number in the file.</param>
    void LogError(
        string message,
        string? title = null,
        string? file = null,
        int? startLine = null,
        int? endLine = null,
        int? startColumn = null,
        int? endColumn = null
    );

    /// <summary>
    /// Starts an expandable group in the GitHub Actions log.
    /// </summary>
    /// <param name="title">The title of the group.</param>
    void BeginGroup(string title);

    /// <summary>
    /// Completes the current expandable group in the GitHub Actions log.
    /// </summary>
    void EndGroup();

    /// <summary>
    /// Writes content to the job summary for the GitHub Actions run.
    /// </summary>
    /// <param name="summary">The summary text to write. GitHub flavored Markdown is supported.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    Task AppendJobSummary(string summary, CancellationToken cancellationToken = default);
}
