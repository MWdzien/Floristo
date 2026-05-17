using Floristo.Domain.Products.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Products.Events;

public record ProductGiftTagDisallowedEvent(ProductId Id) : IDomainEvent;