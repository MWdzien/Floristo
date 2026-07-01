using Floristo.Domain.Categories.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Categories.Events;

public record CategoryCreatedEvent(CategoryId Id, CategoryId? ParentId, CategoryName Name, CategorySlug Slug, CategorySortOrder SortOrder, 
    CategoryCreatedAt CreatedAt) : IDomainEvent;