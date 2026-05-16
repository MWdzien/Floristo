namespace Floristo.Application.Customers.Services;

public interface ICustomerReadService
{
    Task<bool> ExistsByEmailAsync(string email, Guid? exceludeId = null);
}