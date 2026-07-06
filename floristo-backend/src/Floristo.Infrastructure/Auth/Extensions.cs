using System.Security.Claims;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Floristo.Application.Auth;
using Google.Apis.Auth.OAuth2;
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
    public static IServiceCollection AddFirebaseAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection(FirebaseAuthOptions.SectionName).Get<FirebaseAuthOptions>()
                      ?? throw new InvalidOperationException("Firebase auth configuration is missing");

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

        services.AddTransient<IClaimsTransformation, FirebaseClaimsTransformation>();
        services.AddScoped<ICurrentUser, CurrentUser>();

        services.AddSingleton(FirebaseApp.Create(new AppOptions
        {
            ProjectId = options.ProjectId,
            Credential = GoogleCredential.GetApplicationDefault()
        }));
        
        services.AddSingleton(FirebaseAuth.DefaultInstance);
        services.AddScoped<IIdentityCustomerLinker, FirebaseIdentityCustomerLinker>();

        return services;
    }
}