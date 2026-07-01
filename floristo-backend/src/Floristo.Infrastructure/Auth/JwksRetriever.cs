using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Floristo.Infrastructure.Auth;

internal sealed class JwksRetriever : IConfigurationRetriever<OpenIdConnectConfiguration>
{
    public async Task<OpenIdConnectConfiguration> GetConfigurationAsync(string address, IDocumentRetriever retriever, CancellationToken cancel)
    {
        var jwksJson = await retriever.GetDocumentAsync(address, cancel);
        var jwks = new JsonWebKeySet(jwksJson);

        var config = new OpenIdConnectConfiguration();
        foreach (var key in jwks.GetSigningKeys())
        {
            config.SigningKeys.Add(key);
        }

        return config;
    }
}