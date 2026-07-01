using Floristo.Application.Products.DTOs;
using Floristo.SharedAbstractions.Queries;

namespace Floristo.Application.Products.Queries;

public record GetProductForBackoffice(Guid Id) : IQuery<BackofficeProductDetailsDto>;