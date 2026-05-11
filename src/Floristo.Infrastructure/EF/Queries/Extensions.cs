using Floristo.Application.DTOs;
using Floristo.Infrastructure.EF.Models;

namespace Floristo.Infrastructure.EF.Queries;

internal static class Extensions
{
    internal static CustomerDto AsDto(this CustomerReadModel customer)                                                                                                                                             
        => new(customer.Id, customer.FirstName, customer.LastName,
            customer.Email, customer.CreatedAt, customer.DeactivatedAt);
}