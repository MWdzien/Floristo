using Floristo.Domain.Events;
using Floristo.Domain.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Entities;

public class Customer : AggregateRoot<CustomerId>
{
    private CustomerFirstName _firstName;
    private CustomerLastName _lastName;
    private CustomerEmail _email;
    private CustomerCreatedAt _createdAt;

    internal Customer(CustomerId id, CustomerFirstName firstName, CustomerLastName lastName, CustomerEmail email, CustomerCreatedAt createdAt)
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
        AddEvent(new CustomerFirstNameChanged(this.Id, firstName));
    }

    public void ChangeLastName(CustomerLastName lastName)
    {
        _lastName = lastName;
        AddEvent(new CustomerLastNameChanged(this.Id, lastName));
    }

    public void ChangeEmail(CustomerEmail email)
    {
        _email = email;
        AddEvent(new CustomerEmailChanged(this.Id, email));
    }
}