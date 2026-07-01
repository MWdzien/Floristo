using Floristo.Domain.Products.ValueObjects;
using Floristo.Domain.Products.ValueObjects.ProductImage;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Products.Events;

public record ProductImageRemovedEvent(ProductId Id, ProductImageId ImageId) : IDomainEvent;