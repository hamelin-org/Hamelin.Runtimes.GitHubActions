using Microsoft.Extensions.Logging;

namespace Hamelin.Runtimes.GitHubActions.Logging;

internal class GitHubActionsLoggerProvider(
    IGitHubActionsCommands commands,
    IExternalScopeProvider? scopeProvider
) : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName) => new GitHubActionsLogger(commands, scopeProvider);

    public void Dispose()
    {
        // Nothing to dispose.
    }
}
