using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
        return services;
    }
}
