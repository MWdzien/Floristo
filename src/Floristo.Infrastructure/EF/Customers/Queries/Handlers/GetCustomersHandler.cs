using Floristo.Application.Customers.DTOs;
using Floristo.Application.Customers.Queries;
using Floristo.Infrastructure.EF.Contexts;
using Floristo.Infrastructure.EF.Customers.Models;
using Floristo.SharedAbstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace Floristo.Infrastructure.EF.Customers.Queries.Handlers;

internal sealed class GetCustomersHandler : IQueryHandler<GetCustomers, IEnumerable<CustomerDto>>
{
    private readonly DbSet<CustomerReadModel> _customers;

    public GetCustomersHandler(ReadDbContext dbContext)
        => _customers = dbContext.Customers;
    
    public async Task<IEnumerable<CustomerDto>> HandleAsync(GetCustomers query)
    {
        var customers = _customers.AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.SearchPhrase))
        {
            var searchPhrase = query.SearchPhrase;
            customers = customers.Where(c =>
                Microsoft.EntityFrameworkCore.EF.Functions.ILike(c.FirstName, $"%{searchPhrase}%") ||
                Microsoft.EntityFrameworkCore.EF.Functions.ILike(c.LastName, $"%{searchPhrase}%") ||
                Microsoft.EntityFrameworkCore.EF.Functions.ILike(c.Email, $"%{searchPhrase}%")
            );
        }

        if (query.IsActive.HasValue)
        {
            customers = query.IsActive.Value
                ? customers.Where(c => c.DeactivatedAt == null)
                : customers.Where(c => c.DeactivatedAt != null);
        }
        
        return await customers.AsNoTracking().Select(c => c.AsDto()).ToListAsync();
    }
}