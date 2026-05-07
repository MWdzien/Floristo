using Floristo.Domain.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Events;

public record CustomerLastNameChanged(CustomerId Id, CustomerLastName LastName) : IDomainEvent;