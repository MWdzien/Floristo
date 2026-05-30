using Floristo.Application.Customers.DTOs;
using Floristo.SharedAbstractions.Queries;

namespace Floristo.Application.Customers.Queries;

public record GetCustomersForBackoffice(string? SearchPhrase, bool? IsActive) : IQuery<IEnumerable<BackofficeCustomerDto>>;