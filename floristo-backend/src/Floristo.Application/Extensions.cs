using Floristo.Domain.Categories.Factories;
using Floristo.Domain.Customers.Factories;
using Floristo.Domain.Products.Factories;
using Floristo.Shared.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Floristo.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddCommands();
        services.AddSingleton<ICustomerFactory, CustomerFactory>();
        services.AddSingleton<ICategoryFactory, CategoryFactory>();
        services.AddSingleton<IProductFactory, ProductFactory>();
        
        return services;
    }
}