using Floristo.Application.Customers.DTOs;
using Floristo.SharedAbstractions.Queries;

namespace Floristo.Application.Customers.Queries;

public record GetCurrentCustomer(Guid CustomerId) : IQuery<StorefrontCustomerDto?>;