using Floristo.Domain.Carts.ValueObjects.Carts;
using Floristo.Domain.Products.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Carts.Events;

public record CartProductRemovedEvent(CartId CartId, ProductId ProductId) : IDomainEvent;