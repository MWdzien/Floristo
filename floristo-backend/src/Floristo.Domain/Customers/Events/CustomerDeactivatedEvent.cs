using Floristo.Domain.Customers.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Customers.Events;

public record CustomerDeactivatedEvent(CustomerId CustomerId, CustomerDeactivatedAt DeactivatedAt) : IDomainEvent;