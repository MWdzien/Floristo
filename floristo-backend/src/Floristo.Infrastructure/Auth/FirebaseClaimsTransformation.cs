using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace Floristo.Infrastructure.Auth;

public class FirebaseClaimsTransformation : IClaimsTransformation
{
    private const string RoleClaimType = "role";
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

        var role = identity.FindFirst(RoleClaimType)?.Value ?? DefaultRole;
        identity.AddClaim(new Claim(ClaimTypes.Role, role));

        return Task.FromResult(principal);
    }
}