namespace Hamelin.Runtimes.GitHubActions.Tests.Unit;

public class GitHubActionsCommandsTests
{
    private readonly StringWriter _writer = new();
    private readonly GitHubActionsCommands _sut = new();

    public GitHubActionsCommandsTests()
    {
        Console.SetOut(_writer);
    }

    [Fact]
    public void LogDebug_Message_LogsDebugMessage()
    {
        // Arrange

        // Act
        _sut.LogDebug("This is a debug message");

        // Assert
        string output = _writer.ToString();
        output.ShouldBe("::debug::This is a debug message\n");
    }

    [Fact]
    public void LogNotice_AllArgs_LogsNotice()
    {
        // Arrange

        // Act
        _sut.LogNotice(
            message: "This is a notice message",
            title: "Title",
            file: "file.txt",
            startLine: 1,
            endLine: 2,
            startColumn: 3,
            endColumn: 4
        );

        // Assert
        string output = _writer.ToString();
        output.ShouldBe("::notice title=Title,file=file.txt,line=1,endLine=2,col=3,endColumn=4::This is a notice message\n");
    }

    [Fact]
    public void LogNotice_NoOptionalArgs_LogsNotice()
    {
        // Arrange

        // Act
        _sut.LogNotice("This is a notice message");

        // Assert
        string output = _writer.ToString();
        output.ShouldBe("::notice::This is a notice message\n");
    }

    [Fact]
    public void LogWarning_AllArgs_LogsNotice()
    {
        // Arrange

        // Act
        _sut.LogWarning(
            message: "This is a warning message",
            title: "Title",
            file: "file.txt",
            startLine: 1,
            endLine: 2,
            startColumn: 3,
            endColumn: 4
        );

        // Assert
        string output = _writer.ToString();
        output.ShouldBe("::warning title=Title,file=file.txt,line=1,endLine=2,col=3,endColumn=4::This is a warning message\n");
    }

    [Fact]
    public void LogWarning_NoOptionalArgs_LogsNotice()
    {
        // Arrange

        // Acts
        _sut.LogWarning("This is a warning message");

        // Assert
        string output = _writer.ToString();
        output.ShouldBe("::warning::This is a warning message\n");
    }

    [Fact]
    public void LogError_AllArgs_LogsNotice()
    {
        // Arrange

        // Act
        _sut.LogError(
            message: "This is an error message",
            title: "Title",
            file: "file.txt",
            startLine: 1,
            endLine: 2,
            startColumn: 3,
            endColumn: 4
        );

        // Assert
        string output = _writer.ToString();
        output.ShouldBe("::error title=Title,file=file.txt,line=1,endLine=2,col=3,endColumn=4::This is an error message\n");
    }

    [Fact]
    public void LogError_NoOptionalArgs_LogsNotice()
    {
        // Arrange

        // Acts
        _sut.LogError("This is an error message");

        // Assert
        string output = _writer.ToString();
        output.ShouldBe("::error::This is an error message\n");
    }

    [Fact]
    public void BeginGroup_WithTitle_LogsCommand()
    {
        // Arrange

        // Act
        _sut.BeginGroup("Title");

        // Assert
        string output = _writer.ToString();
        output.ShouldBe("::group::Title\n");
    }

    [Fact]
    public void EndGroup_LogsCommand()
    {
        // Arrange

        // Act
        _sut.EndGroup();

        // Assert
        string output = _writer.ToString();
        output.ShouldBe("::endgroup::\n");
    }

    [Fact]
    public void SetJobSummary_SetsEnvironmentVariable()
    {
        // Arrange

        // Act
        _sut.SetJobSummary("### Hello world! :rocket:");

        // Assert
        string? output = Environment.GetEnvironmentVariable("GITHUB_STEP_SUMMARY");
        output.ShouldBe("### Hello world! :rocket:");
    }
}
