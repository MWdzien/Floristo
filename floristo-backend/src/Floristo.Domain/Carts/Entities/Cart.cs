using Floristo.Domain.Carts.Events;
using Floristo.Domain.Carts.Exceptions;
using Floristo.Domain.Carts.ValueObjects.CartProducts;
using Floristo.Domain.Carts.ValueObjects.Carts;
using Floristo.Domain.Customers.ValueObjects;
using Floristo.Domain.Products.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Carts.Entities;

public class Cart : AggregateRoot<CartId>
{
    private CustomerId _customerId;
    private CartCreatedAt _createdAt;
    
    private readonly List<CartProduct> _products = new();
    public IReadOnlyCollection<CartProduct> Products => _products.AsReadOnly();

    private Cart()
    {
    }

    internal Cart(CartId id, CustomerId customerId, CartCreatedAt createdAt)
    {
        Id = id;
        _customerId = customerId;
        _createdAt = createdAt;
        AddEvent(new CartCreatedEvent(Id, _customerId, _createdAt));
    }

    internal Cart(CartId id, CustomerId customerId, CartCreatedAt createdAt, List<CartProduct> products)
    {
        Id = id;
        _customerId = customerId;
        _createdAt = createdAt;
        _products = new List<CartProduct>(products);
    }

    public void AddProduct(ProductId productId)
    {
        var cartProduct = _products.SingleOrDefault(p => p.ProductId == productId);
        
        if (cartProduct is not null)
        {
            cartProduct.IncreaseQuantity();
            AddEvent(new CartProductQuantityChangedEvent(Id, productId));
            return;
        }
        
        _products.Add(new CartProduct(productId, 1));
        AddEvent(new CartProductAddedEvent(Id, productId));
    }

    public void RemoveProduct(ProductId productId)
    {
        var cartProduct = _products.SingleOrDefault(p => p.ProductId == productId);
        
        if (cartProduct is null)
        {
            throw new CartProductNotFoundException(productId, Id);
        }

        _products.Remove(cartProduct);
        AddEvent(new CartProductRemovedEvent(Id, productId));
    }

    public void SetProductQuantity(ProductId productId, CartProductQuantity quantity)
    {
        var cartProduct = _products.SingleOrDefault(p => p.ProductId == productId);

        if (cartProduct is null)
        {
            throw new CartProductNotFoundException(productId, Id);
        }

        if (quantity == 0)
        {
            RemoveProduct(productId);
            return;
        }

        cartProduct.SetQuantity(quantity);
        AddEvent(new CartProductQuantityChangedEvent(Id, productId));
    }
}