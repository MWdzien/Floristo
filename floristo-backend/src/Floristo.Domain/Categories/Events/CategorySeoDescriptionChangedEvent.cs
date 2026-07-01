using Floristo.Domain.Categories.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Categories.Events;

public record CategorySeoDescriptionChangedEvent(CategoryId Id, CategorySeoDescription? SeoDescription) : IDomainEvent;