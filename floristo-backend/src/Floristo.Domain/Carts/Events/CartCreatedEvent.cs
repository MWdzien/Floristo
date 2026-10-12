using Floristo.Domain.Carts.ValueObjects.Carts;
using Floristo.Domain.Customers.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Carts.Events;

public record CartCreatedEvent(CartId CartId, CustomerId CustomerId, CartCreatedAt CartCreatedAt) : IDomainEvent;