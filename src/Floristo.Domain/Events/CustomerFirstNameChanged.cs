using Floristo.Domain.Entities;
using Floristo.Domain.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Events;

public record CustomerFirstNameChanged(Customer Customer, CustomerFirstName CustomerFirstName) : IDomainEvent;