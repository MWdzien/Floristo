using Floristo.Domain.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Events;

public record CustomerFirstNameChangedEvent(CustomerId Id, CustomerFirstName CustomerFirstName) : IDomainEvent;