namespace Floristo.Application.Auth;

public interface IIdentityCustomerLinker
{
    Task LinkCustomerAsync(string identityId, Guid customerId);
}