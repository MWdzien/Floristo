using System.Reflection;
using Floristo.SharedAbstractions.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Floristo.Shared.Queries;

public static class Extensions
{
    public static IServiceCollection AddQueries(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();
        
        services.AddSingleton<IQueryDispatcher, InMemoryQueryDispatcher>();
        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)), publicOnly: false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        return services;
    }
}