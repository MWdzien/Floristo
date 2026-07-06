using System.Security.Claims;
using Floristo.Application.Auth;
using Floristo.Application.Customers.Services;
using Microsoft.AspNetCore.Http;

namespace Floristo.Infrastructure.Auth;

internal sealed class CurrentUser : ICurrentUser
{
    private const string CustomerIdClaimType = "customer_id";
    private const string DefaultRole = "customer";
    private readonly IHttpContextAccessor _accessor;
    private readonly ICustomerReadService _customerReadService;
    private Guid? _cachedId;

    public CurrentUser(IHttpContextAccessor accessor, ICustomerReadService customerReadService)
    {
        _accessor = accessor;
        _customerReadService = customerReadService;
    }

    private ClaimsPrincipal? Principal => _accessor.HttpContext?.User;
    public bool IsAuthenticated => Principal?.Identity?.IsAuthenticated ?? false;

    public string IdentityId
    {
        get
        {
            if (!IsAuthenticated)
                throw new InvalidOperationException("Cannot read IdentityId of unauthenticated user");
            
            var sub = Principal!.FindFirst("sub")?.Value;

            if (string.IsNullOrWhiteSpace(sub))
                throw new InvalidOperationException("Authenticated user has no 'sub' claim");

            return sub;
        }
    }

    public Guid Id
    {
        get
        {
            if (_cachedId.HasValue) return _cachedId.Value;
            
            if (!IsAuthenticated)
                throw new InvalidOperationException("Cannot read Id of unauthenticated user");
            
            var claim = Principal!.FindFirst(CustomerIdClaimType)?.Value;
            if (Guid.TryParse(claim, out var fromClaim))
            {
                _cachedId = fromClaim;
                return fromClaim;
            }

            var fromDb = _customerReadService.GetIdByIdentityIdAsync(IdentityId).GetAwaiter().GetResult();
            if (fromDb == null)
                throw new InvalidOperationException("Current user has no provisioned customer");
            
            _cachedId = fromDb;
            return fromDb.Value;
        }
    }

    public string? Email => Principal?.FindFirst("email")?.Value;
    public string Role => Principal?.FindFirst(ClaimTypes.Role)?.Value ?? DefaultRole;
}