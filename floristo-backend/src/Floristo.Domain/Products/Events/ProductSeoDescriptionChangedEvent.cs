using Floristo.Domain.Products.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Products.Events;

public record ProductSeoDescriptionChangedEvent(ProductId Id, ProductSeoDescription? SeoDescription) : IDomainEvent;