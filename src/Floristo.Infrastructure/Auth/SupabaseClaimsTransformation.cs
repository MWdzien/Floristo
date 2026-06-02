using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;

namespace Floristo.Infrastructure.Auth;

public class SupabaseClaimsTransformation : IClaimsTransformation
{
    private const string AppMetadataClaimType = "app_metadata";
    private const string DefaultRole = "customer";
    
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (principal.Identity is not ClaimsIdentity identity || !identity.IsAuthenticated)
        {
            return Task.FromResult(principal);
        }

        if (principal.HasClaim(c => c.Type == ClaimTypes.Role))
        {
            return Task.FromResult(principal);
        }

        var role = ExtractRole(identity) ?? DefaultRole;
        identity.AddClaim(new Claim(ClaimTypes.Role, role));

        return Task.FromResult(principal);
    }

    public static string? ExtractRole(ClaimsIdentity identity)
    {
        var appMetadataJson = identity.FindFirst(AppMetadataClaimType)?.Value;

        if (string.IsNullOrWhiteSpace(appMetadataJson))
        {
            return null;
        }

        try
        {
            using var doc = JsonDocument.Parse(appMetadataJson);
            if (doc.RootElement.TryGetProperty("role", out var roleProp)
                && roleProp.ValueKind == JsonValueKind.String)
            {
                return roleProp.GetString();
            }
        }
        catch (JsonException)
        {
            return null;
        }

        return null;
    }
}