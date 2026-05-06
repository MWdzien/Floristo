using Floristo.Domain.Entities;
using Floristo.Domain.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Events;

public record CustomerEmailChanged(Customer Customer, CustomerEmail Email) : IDomainEvent;