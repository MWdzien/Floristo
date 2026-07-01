using Floristo.Infrastructure.EF;
using Floristo.Infrastructure.Logging;
using Floristo.Shared.Queries;
using Floristo.SharedAbstractions.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Floristo.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPostgres(configuration);
        
        services.AddQueries();
        services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));
        
        return services;
    }
}