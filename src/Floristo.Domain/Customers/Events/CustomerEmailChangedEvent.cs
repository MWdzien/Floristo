using Floristo.Domain.Customers.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Customers.Events;

public record CustomerEmailChangedEvent(CustomerId Id, CustomerEmail Email) : IDomainEvent;