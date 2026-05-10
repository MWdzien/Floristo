using Floristo.Domain.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Events;

public record CustomerDeactivatedEvent(CustomerId CustomerId, CustomerDeactivatedAt DeactivatedAt) : IDomainEvent;