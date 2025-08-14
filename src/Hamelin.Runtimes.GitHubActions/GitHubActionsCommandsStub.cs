namespace Hamelin.Runtimes.GitHubActions;

internal class GitHubActionsCommandsStub : IGitHubActionsCommands
{
    public void LogDebug(string message) { }

    public void LogNotice(string message, string? title = null, string? file = null, int? startLine = null,
        int? endLine = null, int? startColumn = null, int? endColumn = null)
    { }

    public void LogWarning(string message, string? title = null, string? file = null, int? startLine = null,
        int? endLine = null, int? startColumn = null, int? endColumn = null)
    { }

    public void LogError(string message, string? title = null, string? file = null, int? startLine = null,
        int? endLine = null, int? startColumn = null, int? endColumn = null)
    { }

    public void BeginGroup(string title) { }

    public void EndGroup() { }

    public Task AppendJobSummary(string summary, CancellationToken cancellationToken = default) => Task.CompletedTask;
}
