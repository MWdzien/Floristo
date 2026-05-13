using Floristo.Domain.Customers.ValueObjects;

namespace Floristo.Domain.Customers.Factories;

public interface ICustomerFactory
{
    Entities.Customer Create(CustomerId id, CustomerFirstName firstName, CustomerLastName lastName, CustomerEmail email);
    Entities.Customer Restore(CustomerId id, CustomerFirstName firstName, CustomerLastName lastName, CustomerEmail email, CustomerCreatedAt createdAt);
}