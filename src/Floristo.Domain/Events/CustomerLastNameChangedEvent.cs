using Floristo.Domain.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Events;

public record CustomerLastNameChangedEvent(CustomerId Id, CustomerLastName LastName) : IDomainEvent;