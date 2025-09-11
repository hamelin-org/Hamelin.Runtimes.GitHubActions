using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;

namespace Hamelin.Runtimes.GitHubActions.Logging;

internal class GitHubActionsConsoleFormatter() : ConsoleFormatter(Constants.FormatterName)
{
    private const string UrlEncodedNewLine = "%0A";

    public override void Write<TState>(
        in LogEntry<TState> logEntry,
        IExternalScopeProvider? scopeProvider,
        TextWriter textWriter
    )
    {
        switch (logEntry.LogLevel)
        {
            case LogLevel.Critical:
            case LogLevel.Error:
                textWriter.Write("::error::");
                break;
            case LogLevel.Warning:
                textWriter.Write("::warning::");
                break;
            case LogLevel.Information:
                // No special formatting for Information level
                textWriter.Write("Information: ");
                break;
            case LogLevel.Debug:
            case LogLevel.Trace:
                // The debug command means debug messages will respect GitHub's debug logging.
                textWriter.Write("::debug::");
                // break;
                break;
            case LogLevel.None:
            default:
                break;
        }

        string message = logEntry.Formatter.Invoke(logEntry.State, logEntry.Exception);
        textWriter.Write(message);

        if (logEntry.Exception != null)
        {
            textWriter.Write(UrlEncodedNewLine);
            string exceptionMessage = logEntry.Exception.ToString()
                .Replace("\r", "")
                .Replace("\n", UrlEncodedNewLine);
            textWriter.Write(exceptionMessage);
        }
        textWriter.WriteLine();
    }
}
