using Floristo.Domain.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Events;

public record CustomerEmailChanged(CustomerId Id, CustomerEmail Email) : IDomainEvent;