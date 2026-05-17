using Floristo.Domain.Products.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Products.Events;

public record ProductSeoTitleChangedEvent(ProductId Id, ProductSeoTitle? SeoTitle) : IDomainEvent;