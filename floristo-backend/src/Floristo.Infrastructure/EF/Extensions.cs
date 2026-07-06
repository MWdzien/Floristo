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
using Floristo.SharedAbstractions.Secrets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

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
        
        var options = configuration.GetOptions<PostgresOptions>(PostgresOptions.SectionName);

        using var tmpProvider = services.BuildServiceProvider();
        var secrets = tmpProvider.GetRequiredService<ISecretsProvider>();
        var password = secrets.GetSecretAsync(options.PasswordSecretName).GetAwaiter().GetResult();
        
        var connectionString = BuildConnectionString(options, password);
        
        services.AddDbContext<ReadDbContext>(ctx => ctx.UseNpgsql(connectionString));
        services.AddDbContext<WriteDbContext>(ctx => ctx.UseNpgsql(connectionString));
        
        return services;
    }

    private static string BuildConnectionString(PostgresOptions options, string password)
    {
        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = options.Host,
            Database = options.Database,
            Username = options.Username,
            Password = password,
            SslMode = Enum.Parse<SslMode>(options.SslMode)
        };

        if (!options.Host.StartsWith('/'))                                                                                                                                                                                 
        {                                                                                                                                                                                                                  
            builder.Port = options.Port;
        }
        
        return builder.ConnectionString;
    }
}