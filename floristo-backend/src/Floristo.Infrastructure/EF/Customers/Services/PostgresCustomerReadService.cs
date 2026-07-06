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

    public Task<bool> ExistsByIdAsync(Guid id)
        => _customers.AnyAsync(c => c.Id == id);

    public Task<Guid?> GetIdByIdentityIdAsync(string identityId)
        => _customers
            .Where(c => c.IdentityId == identityId)
            .Select(c => c.Id as Guid?)
            .SingleOrDefaultAsync();
}