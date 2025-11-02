using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;

namespace Hamelin.Runtimes.GitHubActions.Logging;

internal class GitHubActionsConsoleFormatter() : ConsoleFormatter(Constants.FormatterName)
{
    public override void Write<TState>(
        in LogEntry<TState> logEntry,
        IExternalScopeProvider? scopeProvider,
        TextWriter textWriter
    )
    {
        // Check if this is a raw GitHub Actions command
        // Important to check this by NAME because EventId equality goes by the numeric Id.
        if (logEntry.EventId.Name == Constants.RawCommandEventId.Name)
        {
            // Write the message directly without any formatting
            string rawMessage = logEntry.Formatter.Invoke(logEntry.State, logEntry.Exception);
            textWriter.WriteLine(rawMessage);
            return;
        }

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
        message = StringUtils.SanitizeNewLines(message);
        textWriter.Write(message);

        if (logEntry.Exception != null)
        {
            textWriter.Write(StringUtils.UrlEncodedNewLine);
            string exceptionMessage = StringUtils.SanitizeNewLines(logEntry.Exception.ToString());
            textWriter.Write(exceptionMessage);
        }
        textWriter.WriteLine();
    }
}
