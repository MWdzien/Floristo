using Floristo.Domain.Customers.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Customers.Events;

public record CustomerCreatedEvent(CustomerId CustomerId, CustomerFirstName FirstName, CustomerLastName LastName, CustomerEmail Email, CustomerCreatedAt CreatedAt) : IDomainEvent;