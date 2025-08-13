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
    /// Adds GitHub Actions runtime services to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to add the services to.</param>
    /// <returns>The updated service collection with GitHub Actions runtime services added.</returns>
    public static IServiceCollection AddGitHubActionsRuntime(this IServiceCollection services)
    {
        services.TryAddSingleton<IGitHubActionsCommands, GitHubActionsCommands>();

        services.TryAddEnumerable(ServiceDescriptor.Singleton<ConsoleFormatter, GitHubActionsConsoleFormatter>());
        services.Configure<ConsoleLoggerOptions>(o => o.FormatterName = Constants.FormatterName);

        return services;
    }
}
