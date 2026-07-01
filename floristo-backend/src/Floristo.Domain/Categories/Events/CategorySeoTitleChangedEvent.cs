using Floristo.Domain.Categories.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Categories.Events;

public record CategorySeoTitleChangedEvent(CategoryId Id, CategorySeoTitle? SeoTitle) : IDomainEvent;