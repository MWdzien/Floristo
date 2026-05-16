using Floristo.Application.Customers.Services;
using Floristo.Infrastructure.EF.Contexts;
using Floristo.Infrastructure.EF.Customers.Models;
using Microsoft.EntityFrameworkCore;

namespace Floristo.Infrastructure.EF.Customers.Services;

internal sealed class PostgresCustomerReadService : ICustomerReadService
{
    private readonly DbSet<CustomerReadModel> _customers;

    public PostgresCustomerReadService(ReadDbContext dbContext)
    {
        _customers = dbContext.Customers;
    }

    public Task<bool> ExistsByEmailAsync(string email, Guid? exceludeId = null)
        => _customers.AnyAsync(x => x.Email == email && (exceludeId == null || x.Id == exceludeId));
}