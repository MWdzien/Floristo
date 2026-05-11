using Floristo.Domain.Factories;
using Floristo.Shared;
using Floristo.Shared.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Floristo.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddCommands();
        services.AddSingleton<ICustomerFactory, CustomerFactory>();
        
        //auto register policies
        // services.Scan(b => b.FromAssemblies(typeof(XyzPolicy).Assembly)
        //     .AddClasses(classes => classes.AssignableTo<XyzPolicy>())
        //     .AsImplementedInterfaces()
        //     .WithScopedLifetime());
        
        return services;
    }
}