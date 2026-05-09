using Floristo.Shared.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Floristo.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddQueries();
        
        return services;
    }
}