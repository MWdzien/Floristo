using Floristo.Domain.Customers.ValueObjects;

namespace Floristo.Domain.Customers.Factories;

public sealed class CustomerFactory : ICustomerFactory
{
    public Entities.Customer Create(CustomerId id, CustomerFirstName firstName, CustomerLastName lastName, CustomerEmail email)
        => new(id, firstName, lastName, email);

    public Entities.Customer Restore(CustomerId id, CustomerFirstName firstName, CustomerLastName lastName, CustomerEmail email,
        CustomerCreatedAt createdAt)
        => new(id, firstName, lastName, email, createdAt);
}