using Floristo.Shared.Options;
using Floristo.SharedAbstractions.Secrets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Floristo.Infrastructure.Secrets;

public static class Extensions
{
    internal static IServiceCollection AddGoogleSecrets(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetOptions<GoogleSecretsOptions>(GoogleSecretsOptions.SectionName);

        services.AddSingleton(options);
        services.AddSingleton<ISecretsProvider, GoogleSecretsProvider>();
        
        return services;
    }
}