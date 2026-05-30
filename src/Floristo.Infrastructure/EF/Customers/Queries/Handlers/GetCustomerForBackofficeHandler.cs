using Floristo.Application.Customers.DTOs;
using Floristo.Application.Customers.Queries;
using Floristo.Infrastructure.EF.Contexts;
using Floristo.Infrastructure.EF.Customers.Models;
using Floristo.SharedAbstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace Floristo.Infrastructure.EF.Customers.Queries.Handlers;

internal sealed class GetCustomerForBackofficeHandler : IQueryHandler<GetCustomerForBackoffice, BackofficeCustomerDetailsDto?>
{
    private readonly DbSet<CustomerReadModel> _customers;

    public GetCustomerForBackofficeHandler(ReadDbContext dbContext)
        => _customers = dbContext.Customers;

    public Task<BackofficeCustomerDetailsDto?> HandleAsync(GetCustomerForBackoffice query)
        => _customers
            .Where(c => c.Id == query.Id)
            .AsNoTracking()
            .Select(c => c.AsDto())
            .SingleOrDefaultAsync();
}