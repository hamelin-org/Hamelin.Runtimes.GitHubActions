using Hamelin.Runtimes.GitHubActions.Logging;
using Microsoft.Extensions.Logging;

namespace Hamelin.Runtimes.GitHubActions.Tests.Unit.Logging;

public class GitHubActionsLoggerTests
{
    [Fact]
    public void LogWarning_Text_ShouldLog()
    {
        // Arrange
        var commandsMock = Substitute.For<IGitHubActionsCommands>();
        var logger = new GitHubActionsLogger(commandsMock, null);

        // Act
        logger.LogWarning("Test warning");

        // Assert
        commandsMock.Received().LogWarning("Test warning");
    }

    [Fact]
    public void LogError_WithException_ShouldLog()
    {
        // Arrange
        var commandsMock = Substitute.For<IGitHubActionsCommands>();
        var logger = new GitHubActionsLogger(commandsMock, null);
        var ex = new Exception("Test exception");

        // Act
        logger.LogError(ex, "Test error");

        // Assert
        commandsMock.Received().LogError(Arg.Is<string>(message => message.Contains("Test error") && message.Contains("Test exception")));
    }
}
