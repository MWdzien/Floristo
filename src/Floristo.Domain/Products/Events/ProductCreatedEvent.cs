using Floristo.Domain.Categories.ValueObjects;
using Floristo.Domain.Products.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Products.Events;

public record ProductCreatedEvent(ProductId Id, ProductName Name, ProductSlug Slug, ProductPrice Price, 
    CategoryId CategoryId, bool AllowEngraving, bool  AllowGiftTag, bool IsFeatured, ProductSortOrder SortOrder, 
    ProductCreatedAt CreatedAt, ProductDeactivatedAt DeactivatedAt) : IDomainEvent;