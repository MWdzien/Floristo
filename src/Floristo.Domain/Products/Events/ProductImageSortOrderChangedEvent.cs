using Floristo.Domain.Products.ValueObjects;
using Floristo.Domain.Products.ValueObjects.ProductImage;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Products.Events;

public record ProductImageSortOrderChangedEvent(ProductId Id, ProductImageId ImageId, ProductImageSortOrder SortOrder) : IDomainEvent;