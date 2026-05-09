using Floristo.Application.DTOs;
using Floristo.SharedAbstractions.Queries;

namespace Floristo.Application.Queries;

public record GetCustomers(string? LastName, string? Email, bool? IsActive) : IQuery<IEnumerable<CustomerDto>>;