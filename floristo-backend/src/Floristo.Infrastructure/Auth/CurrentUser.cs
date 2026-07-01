using System.Security.Claims;
using Floristo.Application.Auth;
using Microsoft.AspNetCore.Http;

namespace Floristo.Infrastructure.Auth;

internal sealed class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _accessor;

    public CurrentUser(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    private ClaimsPrincipal? Principal => _accessor.HttpContext?.User;
    public bool IsAuthenticated => Principal?.Identity?.IsAuthenticated ?? false;

    public Guid Id
    {
        get
        {
            if (!IsAuthenticated)
                throw new InvalidOperationException("Cannot read Id of unauthenticated user");

            var sub = Principal!.FindFirst("sub")?.Value;
            
            if (string.IsNullOrWhiteSpace(sub) || !Guid.TryParse(sub, out var id))
                throw new InvalidOperationException("Authenticated user has no valid 'sub' claim");

            return id;
        }
    }

    public string? Email => Principal?.FindFirst("email")?.Value;
    public string Role => Principal?.FindFirst(ClaimTypes.Role)?.Value ?? "customer";
}