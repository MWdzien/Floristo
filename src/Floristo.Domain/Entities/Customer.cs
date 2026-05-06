using Floristo.Domain.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Entities;

public class Customer
{
    public CustomerId Id { get; private set; }

    private CustomerFirstName _firstName;
    private CustomerLastName _lastName;
    private CustomerEmail _email;
    private DateTime _createdAt;

    internal Customer(Guid id, CustomerFirstName firstName, CustomerLastName lastName, CustomerEmail email, DateTime createdAt)
    {
        Id = id;
        _firstName = firstName;
        _lastName = lastName;
        _email = email;
        _createdAt = createdAt;
    }

    public void ChangeFirstName(CustomerFirstName firstName)
    {
        _firstName = firstName;
    }

    public void ChangeLastName(CustomerLastName lastName)
    {
        _lastName = lastName;
    }

    public void ChangeEmail(CustomerEmail email)
    {
        _email = email;
    }
}