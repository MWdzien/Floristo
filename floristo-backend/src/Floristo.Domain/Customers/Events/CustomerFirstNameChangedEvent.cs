using Floristo.Domain.Customers.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Customers.Events;

public record CustomerFirstNameChangedEvent(CustomerId Id, CustomerFirstName CustomerFirstName) : IDomainEvent;