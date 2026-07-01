using Floristo.Domain.Products.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Products.Events;

public record ProductSlugChangedEvent(ProductId Id, ProductSlug Slug) : IDomainEvent;