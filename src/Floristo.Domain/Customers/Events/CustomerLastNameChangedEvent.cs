using Floristo.Domain.Customers.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Customers.Events;

public record CustomerLastNameChangedEvent(CustomerId Id, CustomerLastName LastName) : IDomainEvent;