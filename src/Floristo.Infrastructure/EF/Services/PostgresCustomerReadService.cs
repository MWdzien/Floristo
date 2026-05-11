using Floristo.Application.Services;
using Floristo.Infrastructure.EF.Contexts;
using Floristo.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Floristo.Infrastructure.EF.Services;

internal sealed class PostgresCustomerReadService : ICustomerReadService
{
    private readonly DbSet<CustomerReadModel> _customers;

    public PostgresCustomerReadService(ReadDbContext dbContext)
    {
        _customers = dbContext.Customers;
    }

    public Task<bool> ExistsByEmailAsync(string email)
        => _customers.AnyAsync(x => x.Email == email);
}