using Floristo.Domain.Products.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Products.Events;

public record ProductDeactivatedEvent(ProductId Id, ProductDeactivatedAt DeactivatedAt) : IDomainEvent;