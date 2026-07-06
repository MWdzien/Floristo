using Floristo.Domain.Customers.Entities;
using Floristo.Domain.Customers.ValueObjects;

namespace Floristo.Domain.Customers.Factories;

public sealed class CustomerFactory : ICustomerFactory
{
    public Customer Create(CustomerId id, CustomerIdentityId identityId, CustomerFirstName firstName, 
        CustomerLastName lastName, CustomerEmail email)
        => new(id, identityId, firstName, lastName, email);

    public Customer Restore(CustomerId id, CustomerIdentityId identityId, CustomerFirstName firstName, 
        CustomerLastName lastName, CustomerEmail email, CustomerCreatedAt createdAt, CustomerDeactivatedAt? deactivatedAt)
        => new(id, identityId, firstName, lastName, email, createdAt, deactivatedAt);
}