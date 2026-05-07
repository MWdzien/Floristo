using Floristo.Domain.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Events;

public record CustomerEmailChangedEvent(CustomerId Id, CustomerEmail Email) : IDomainEvent;