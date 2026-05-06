using Floristo.Domain.Events;
using Floristo.Domain.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Entities;

public class Customer : AggregateRoot<CustomerId>
{
    public CustomerId Id { get; private set; }

    private CustomerFirstName _firstName;
    private CustomerLastName _lastName;
    private CustomerEmail _email;
    private DateTime _createdAt;

    internal Customer(CustomerId id, CustomerFirstName firstName, CustomerLastName lastName, CustomerEmail email, DateTime createdAt)
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
        AddEvent(new CustomerFirstNameChanged(this, firstName));
    }

    public void ChangeLastName(CustomerLastName lastName)
    {
        _lastName = lastName;
        AddEvent(new CustomerLastNameChanged(this, lastName));
    }

    public void ChangeEmail(CustomerEmail email)
    {
        _email = email;
        AddEvent(new CustomerEmailChanged(this, email));
    }
}