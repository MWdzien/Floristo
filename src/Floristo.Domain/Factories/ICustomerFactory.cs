using Floristo.Domain.Entities;
using Floristo.Domain.ValueObjects;

namespace Floristo.Domain.Factories;

public interface ICustomerFactory
{
    Customer Create(CustomerId id, CustomerFirstName firstName, CustomerLastName lastName, CustomerEmail email);
    Customer Restore(CustomerId id, CustomerFirstName firstName, CustomerLastName lastName, CustomerEmail email, CustomerCreatedAt createdAt);
}