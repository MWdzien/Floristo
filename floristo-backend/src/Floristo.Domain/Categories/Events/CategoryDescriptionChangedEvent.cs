using Floristo.Domain.Categories.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Categories.Events;

public record CategoryDescriptionChangedEvent(CategoryId Id, CategoryDescription? Description) : IDomainEvent;