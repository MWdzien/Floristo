using Floristo.Domain.Categories.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Categories.Events;

public record CategoryImageUrlChangedEvent(CategoryId Id, CategoryImageUrl ImageUrl) : IDomainEvent;