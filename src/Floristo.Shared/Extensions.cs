using Floristo.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Floristo.Shared;

public static class Extensions
{
    public static IServiceCollection AddShared(this IServiceCollection services)
    {
        services.AddHostedService<AppInitializer>();
        return services;
    }
}