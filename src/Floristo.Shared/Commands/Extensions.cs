using System.Reflection;
using Floristo.SharedAbstractions.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Floristo.Shared.Commands;

public static class Extensions
{
    public static IServiceCollection AddCommands(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();
        
        services.AddSingleton<ICommandDispatcher, InMemoryCommandDispatcher>();
        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        return services;
    }
}