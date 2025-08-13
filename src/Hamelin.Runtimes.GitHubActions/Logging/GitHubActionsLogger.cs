using Microsoft.Extensions.Logging;

namespace Hamelin.Runtimes.GitHubActions.Logging;

internal class GitHubActionsLogger(
    IGitHubActionsCommands commands,
    IExternalScopeProvider? scopeProvider
) : ILogger
{
    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter
    )
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        string message = formatter(state, exception);
        if (exception != null)
        {
            message += Environment.NewLine + exception;
        }

        switch (logLevel)
        {
            case LogLevel.Critical:
            case LogLevel.Error:
                commands.LogError(message);
                break;
            case LogLevel.Warning:
                commands.LogWarning(message);
                break;
            case LogLevel.Trace:
            case LogLevel.Debug:
                commands.LogDebug(message);
                break;
            case LogLevel.Information:
            case LogLevel.None:
            default:
                // Do nothing.
                break;
        }
    }

    public bool IsEnabled(LogLevel logLevel) => logLevel is not LogLevel.None and not LogLevel.Information;

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => scopeProvider?.Push(state) ?? null;
}
