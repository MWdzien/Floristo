using Floristo.Domain.Customers.Entities;
using Floristo.Domain.Customers.ValueObjects;

namespace Floristo.Domain.Customers.Factories;

public interface ICustomerFactory
{
    Customer Create(CustomerId id, CustomerFirstName firstName, CustomerLastName lastName, CustomerEmail email);
    Customer Restore(CustomerId id, CustomerFirstName firstName, CustomerLastName lastName, CustomerEmail email, CustomerCreatedAt createdAt);
}