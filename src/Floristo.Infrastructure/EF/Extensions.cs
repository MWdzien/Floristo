using Floristo.Domain.Repositories;
using Floristo.Infrastructure.EF.Contexts;
using Floristo.Infrastructure.EF.Options;
using Floristo.Infrastructure.EF.Repositories;
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
        
        var options = configuration.GetOptions<PostgresOptions>("Postgres");
        services.AddDbContext<ReadDbContext>(ctx => ctx.UseNpgsql(options.ConnectionString));
        services.AddDbContext<WriteDbContext>(ctx => ctx.UseNpgsql(options.ConnectionString));
        
        return services;
    }
}