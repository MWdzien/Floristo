using Floristo.Domain.Entities;
using Floristo.Domain.Repositories;
using Floristo.Domain.ValueObjects;
using Floristo.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Floristo.Infrastructure.EF.Repositories;

internal sealed class PostgresCustomerRepository : ICustomerRepository
{
    private readonly DbSet<Customer> _customers;
    private readonly WriteDbContext _dbContext;

    public PostgresCustomerRepository(WriteDbContext dbContext)
    {
        _customers = dbContext.Customers;
        _dbContext = dbContext;
    }

    public Task<Customer?> GetAsync(CustomerId id)
        => _customers.SingleOrDefaultAsync(c => c.Id == id);

    public async Task AddAsync(Customer customer)
    {
        await _customers.AddAsync(customer);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Customer customer)
    {
        _customers.Update(customer);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Customer customer)
    {
        _customers.Remove(customer);
        await _dbContext.SaveChangesAsync();
    }
}