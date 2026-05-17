using Floristo.Domain.Categories.ValueObjects;
using Floristo.Domain.Products.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Products.Events;

public record ProductCategoryChangedEvent(ProductId Id, CategoryId CategoryId) : IDomainEvent;