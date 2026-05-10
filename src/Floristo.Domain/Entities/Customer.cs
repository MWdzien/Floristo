using Floristo.Domain.Events;
using Floristo.Domain.Exceptions;
using Floristo.Domain.Exceptions.Customer;
using Floristo.Domain.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Entities;

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
        AddEvent(new CustomerCreatedEvent(Id, firstName, lastName, email));
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

    public void EnsureActive()
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
    }
}