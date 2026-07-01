using Floristo.Domain.Categories.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Categories.Events;

public record CategorySortOrderChangedEvent(CategoryId Id, CategorySortOrder SortOrder) : IDomainEvent;