using Floristo.Domain.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Events;

public record CustomerCreatedEvent(CustomerId CustomerId, CustomerFirstName FirstName, CustomerLastName LastName, CustomerEmail Email) : IDomainEvent;