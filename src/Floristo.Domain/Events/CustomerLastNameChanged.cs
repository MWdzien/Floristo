using Floristo.Domain.Entities;
using Floristo.Domain.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Events;

public record CustomerLastNameChanged(Customer Customer, CustomerLastName LastName) : IDomainEvent;