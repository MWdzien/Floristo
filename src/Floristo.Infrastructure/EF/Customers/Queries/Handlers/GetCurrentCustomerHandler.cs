using Floristo.Application.Auth;
using Floristo.Application.Customers.DTOs;
using Floristo.Application.Customers.Queries;
using Floristo.Infrastructure.EF.Contexts;
using Floristo.Infrastructure.EF.Customers.Models;
using Floristo.SharedAbstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace Floristo.Infrastructure.EF.Customers.Queries.Handlers;

internal sealed class GetCurrentCustomerHandler : IQueryHandler<GetCurrentCustomer, StorefrontCustomerDto?>
{
    private readonly DbSet<CustomerReadModel> _customers;
    private readonly ICurrentUser _currentUser;

    public GetCurrentCustomerHandler(ReadDbContext dbContext, ICurrentUser currentUser)
    {
        _currentUser = currentUser;
        _customers = dbContext.Customers;
    }

    public async Task<StorefrontCustomerDto?> HandleAsync(GetCurrentCustomer query)
    {
        query = query with { CustomerId = _currentUser.Id };
        
        return await _customers
            .AsNoTracking()
            .Where(c => c.Id == query.CustomerId)
            .Select(c => new StorefrontCustomerDto(c.FirstName, c.LastName, c.Email))
            .SingleOrDefaultAsync();
    }
}