using Hamelin.Runtimes.GitHubActions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging.Console;

namespace Hamelin.Runtimes.GitHubActions;

/// <summary>
/// Provides extension methods for adding GitHub Actions runtime services to the service collection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds GitHub Actions runtime services to the specified service collection if the current runtime environment is GitHub Actions.
    /// </summary>
    /// <param name="services">The service collection to add the services to.</param>
    /// <param name="configure">Controls whether to force service registration regardless of runtime environment.</param>
    /// <returns>The updated service collection with GitHub Actions runtime services added.</returns>
    public static IServiceCollection AddGitHubActionsRuntime(this IServiceCollection services, Action<GitHubActionsRuntimeOptions>? configure = null)
    {
        var options = new GitHubActionsRuntimeOptions();
        configure?.Invoke(options);

        var context = new GitHubActionsContext
        {
            IsEnabled = options.RuntimeDetector()
        };

        services.TryAddSingleton<IGitHubActionsContext>(context);
        services.TryAddEnumerable(ServiceDescriptor.Singleton<ConsoleFormatter, GitHubActionsConsoleFormatter>());

        if (context.IsEnabled)
        {
            services.TryAddSingleton<IGitHubActionsCommands, GitHubActionsCommands>();
            if (options.EnableLogFormatter)
            {
                services.Configure<ConsoleLoggerOptions>(o => o.FormatterName = Constants.FormatterName);
            }
        }
        else
        {
            services.TryAddSingleton<IGitHubActionsCommands, GitHubActionsCommandsStub>();
        }

        return services;
    }
}
