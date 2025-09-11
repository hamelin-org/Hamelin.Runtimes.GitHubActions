using Hamelin.Hooks;

namespace Hamelin.Runtimes.GitHubActions.Logging;

internal class StepGroupingPreStepHook(IGitHubActionsCommands commands) : IPreStepHook
{
    public Task PreStep(PreStepHookArgs args, CancellationToken cancellationToken = new())
    {
        commands.BeginGroup(args.StepName);
        return Task.CompletedTask;
    }
}
