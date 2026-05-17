using Floristo.Domain.Products.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Products.Events;

public record ProductDimensionsChangedEvent(ProductId Id, ProductDimensions? Dimensions) : IDomainEvent;