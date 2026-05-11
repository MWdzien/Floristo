using Floristo.Application.DTOs;
using Floristo.SharedAbstractions.Queries;

namespace Floristo.Application.Queries;

public record GetCustomer(Guid Id) : IQuery<CustomerDto?>;