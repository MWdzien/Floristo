using FirebaseAdmin.Auth;
using Floristo.Application.Auth;

namespace Floristo.Infrastructure.Auth;

internal sealed class FirebaseIdentityCustomerLinker : IIdentityCustomerLinker
{
    private const string CustomerIdClaimType = "customer_id";
    private readonly FirebaseAuth _firebaseAuth;

    public FirebaseIdentityCustomerLinker(FirebaseAuth firebaseAuth)
    {
        _firebaseAuth = firebaseAuth;
    }

    public Task LinkCustomerAsync(string identityId, Guid customerId)
        => _firebaseAuth.SetCustomUserClaimsAsync(
            identityId,
            new Dictionary<string, object>()
            {
                [CustomerIdClaimType] = customerId.ToString()
            }
        );
}