using Floristo.Application.DTOs;
using Floristo.Application.Queries;
using Floristo.Infrastructure.EF.Contexts;
using Floristo.Infrastructure.EF.Models;
using Floristo.SharedAbstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace Floristo.Infrastructure.EF.Queries.Handlers;

internal sealed class GetCustomerHandler : IQueryHandler<GetCustomer, CustomerDto?>
{
    private readonly DbSet<CustomerReadModel> _customers;

    public GetCustomerHandler(ReadDbContext dbContext)
        => _customers = dbContext.Customers;

    public Task<CustomerDto?> HandleAsync(GetCustomer query)
        => _customers
            .Where(c => c.Id == query.Id)
            .AsNoTracking()
            .Select(c => c.AsDto())
            .SingleOrDefaultAsync();
}