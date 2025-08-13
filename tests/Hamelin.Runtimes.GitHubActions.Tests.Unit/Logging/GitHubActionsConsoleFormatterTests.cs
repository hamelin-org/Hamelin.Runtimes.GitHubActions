using Hamelin.Runtimes.GitHubActions.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace Hamelin.Runtimes.GitHubActions.Tests.Unit.Logging;

public class GitHubActionsConsoleFormatterTests
{
    private readonly StringWriter _writer = new();
    private readonly ILogger _logger;
    private readonly ConsoleLoggerProvider _provider;

    public GitHubActionsConsoleFormatterTests()
    {
        Console.SetOut(_writer);

        var formatter = new GitHubActionsConsoleFormatter();

        var options = new ConsoleLoggerOptions { FormatterName = Constants.FormatterName, };
        var monitor = Substitute.For<IOptionsMonitor<ConsoleLoggerOptions>>();
        monitor.CurrentValue.Returns(options);

        _provider = new ConsoleLoggerProvider(monitor, [formatter]);
        _logger = _provider.CreateLogger("TestLogger");
    }

    [Fact]
    public void LogWarning_Text_ShouldLog()
    {
        // Arrange

        // Act
        _logger.LogWarning("Test warning");
        _provider.Dispose();

        // Asserts
        string output = _writer.ToString();
        output.ShouldBe("::warning::Test warning\n");
    }
}
