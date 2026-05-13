using Floristo.Domain.Customers.Factories;
using Floristo.Shared.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Floristo.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddCommands();
        services.AddSingleton<ICustomerFactory, CustomerFactory>();
        
        return services;
    }
}