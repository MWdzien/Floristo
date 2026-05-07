using Floristo.Domain.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Events;

public record CustomerFirstNameChanged(CustomerId Id, CustomerFirstName CustomerFirstName) : IDomainEvent;