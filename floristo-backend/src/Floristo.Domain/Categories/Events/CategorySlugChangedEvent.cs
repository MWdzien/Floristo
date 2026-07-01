using Floristo.Domain.Categories.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Categories.Events;

public record CategorySlugChangedEvent(CategoryId Id, CategorySlug Slug) : IDomainEvent;