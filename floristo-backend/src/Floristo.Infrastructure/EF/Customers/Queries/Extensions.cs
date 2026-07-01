using Floristo.Application.Customers.DTOs;
using Floristo.Infrastructure.EF.Customers.Models;

namespace Floristo.Infrastructure.EF.Customers.Queries;

internal static class Extensions
{
    internal static BackofficeCustomerDetailsDto AsDto(this CustomerReadModel customer)                                                                                                                                             
        => new(customer.Id, customer.FirstName, customer.LastName,
            customer.Email, customer.CreatedAt, customer.DeactivatedAt);
}