using Floristo.Domain.Customers.Events;
using Floristo.Domain.Customers.Exceptions;
using Floristo.Domain.Customers.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Customers.Entities;

public class Customer : AggregateRoot<CustomerId>
{
    private CustomerFirstName _firstName;
    private CustomerLastName _lastName;
    private CustomerEmail _email;
    private CustomerCreatedAt _createdAt;
    private CustomerDeactivatedAt? _deactivatedAt;

    internal Customer(CustomerId id, CustomerFirstName firstName, CustomerLastName lastName, CustomerEmail email)
    {
        SetState(id, firstName, lastName, email);
        _createdAt = DateTime.UtcNow;
        AddEvent(new CustomerCreatedEvent(Id, firstName, lastName, email, _createdAt));
    }

    private Customer()
    {
    }

    internal Customer(CustomerId id, CustomerFirstName firstName, CustomerLastName lastName, CustomerEmail email,
        CustomerCreatedAt createdAt)
    {
        SetState(id, firstName, lastName, email);
        _createdAt = createdAt;
    }

    private void SetState(CustomerId id, CustomerFirstName firstName, CustomerLastName lastName, CustomerEmail email)
    {
        Id = id;
        _firstName = firstName;
        _lastName = lastName;
        _email = email;
    }

    public void ChangeFirstName(CustomerFirstName firstName)
    {
        EnsureActive();
        
        _firstName = firstName;
        AddEvent(new CustomerFirstNameChangedEvent(this.Id, firstName));
    }

    public void ChangeLastName(CustomerLastName lastName)
    {
        EnsureActive();
        
        _lastName = lastName;
        AddEvent(new CustomerLastNameChangedEvent(this.Id, lastName));
    }

    public void ChangeEmail(CustomerEmail email)
    {
        EnsureActive();

        if (_email == email)
        {
            throw new CustomerEmailAlreadyInUseException();
        }
        
        _email = email;
        AddEvent(new CustomerEmailChangedEvent(this.Id, email));
    }

    private void EnsureActive()
    {
        if (_deactivatedAt != null)
        {
            throw new CustomerNotActiveException();
        }
    }

    public void Deactivate()
    {
        if (_deactivatedAt != null)
        {
            throw new CustomerAlreadyDeactivatedException();
        }
        
        _deactivatedAt = new(DateTime.UtcNow);
        AddEvent(new CustomerDeactivatedEvent(this.Id, _deactivatedAt!));
    }
}