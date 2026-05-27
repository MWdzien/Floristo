using Floristo.Application.Categories.Services;
using Floristo.Application.Customers.Services;
using Floristo.Application.Products.Services;
using Floristo.Domain.Categories.Repositories;
using Floristo.Domain.Customers.Repositories;
using Floristo.Domain.Products.Repositories;
using Floristo.Infrastructure.EF.Categories.Repositories;
using Floristo.Infrastructure.EF.Categories.Services;
using Floristo.Infrastructure.EF.Contexts;
using Floristo.Infrastructure.EF.Customers.Repositories;
using Floristo.Infrastructure.EF.Customers.Services;
using Floristo.Infrastructure.EF.Options;
using Floristo.Infrastructure.EF.Products.Repositories;
using Floristo.Infrastructure.EF.Products.Services;
using Floristo.Shared.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Floristo.Infrastructure.EF;

internal static class Extensions
{
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICustomerRepository, PostgresCustomerRepository>();
        services.AddScoped<ICustomerReadService, PostgresCustomerReadService>();
        services.AddScoped<ICategoryRepository, PostgresCategoryRepository>();
        services.AddScoped<ICategoryReadService, PostgresCategoryReadService>();
        services.AddScoped<IProductRepository, PostgresProductRepository>();
        services.AddScoped<IProductReadService, PostgresProductReadService>();
        
        var options = configuration.GetOptions<PostgresOptions>("Postgres");
        services.AddDbContext<ReadDbContext>(ctx => ctx.UseNpgsql(options.ConnectionString));
        services.AddDbContext<WriteDbContext>(ctx => ctx.UseNpgsql(options.ConnectionString));
        
        return services;
    }
}