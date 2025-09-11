using Hamelin.Hooks;

namespace Hamelin.Runtimes.GitHubActions.Logging;

internal class StepGroupingPostStepHook(IGitHubActionsCommands commands) : IPostStepHook
{
    public Task PostStep(PostStepHookArgs args, CancellationToken cancellationToken = new())
    {
        commands.EndGroup();
        return Task.CompletedTask;
    }
}
