using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Floristo.Infrastructure.Auth;

public static class Extensions
{
    public static IServiceCollection AddSupabaseAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection(SupabaseAuthOptions.SectionName).Get<SupabaseAuthOptions>()
                      ?? throw new InvalidOperationException("Supabase auth configuration is missing");

        services.AddSingleton(options);

        var configManager = new ConfigurationManager<OpenIdConnectConfiguration>(
            options.JwksUrl,
            new JwksRetriever(),
            new HttpDocumentRetriever { RequireHttps = true });

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(jwt =>
            {
                jwt.MapInboundClaims = false;

                jwt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = options.Issuer,

                    ValidateAudience = true,
                    ValidAudience = options.Audience,

                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
                    {
                        var config = configManager.GetConfigurationAsync(CancellationToken.None).GetAwaiter()
                            .GetResult();
                        return config.SigningKeys;
                    },

                    NameClaimType = "sub",
                    RoleClaimType = ClaimTypes.Role
                };
            });
        
        services.AddAuthorization();
        services.AddHttpContextAccessor();

        services.AddTransient<IClaimsTransformation, SupabaseClaimsTransformation>();

        return services;
    }
}