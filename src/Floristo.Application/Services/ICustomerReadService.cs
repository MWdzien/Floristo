namespace Floristo.Application.Services;

public interface ICustomerReadService
{
    Task<bool> ExistsByEmailAsync(string email);
}