using Floristo.Domain.Products.ValueObjects;
using Floristo.Domain.Products.ValueObjects.ProductImage;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Products.Events;

public record ProductImageAddedEvent(ProductId Id, ProductImageId ImageId, ProductImageUrl Url, ProductImageSortOrder SortOrder) : IDomainEvent;