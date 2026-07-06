namespace Floristo.Application.Customers.Services;

public interface ICustomerReadService
{
    Task<bool> ExistsByIdAsync(Guid id);
    Task<Guid?> GetIdByIdentityIdAsync(string identityId);
}