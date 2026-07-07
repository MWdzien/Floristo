using Floristo.Domain.Customers.Entities;
using Floristo.Domain.Customers.Events;
using Floristo.Domain.Customers.Exceptions;
using Floristo.Domain.Customers.Factories;
using Shouldly;
using Xunit;

namespace Floristo.Unit.Tests.Domain.CustomerTests;

public class CustomerTests
{
    [Fact]
    public void ChangeFirstName_Throws_CustomerNotActiveException_When_Customer_Is_Deactivated()
    {
        // arrange
        var customer = GetCustomer();
        customer.Deactivate();
        
        // act
        var exception = Record.Exception(() => customer.ChangeFirstName("Tom"));
        
        // assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<CustomerNotActiveException>();
    }

    [Fact]
    public void ChangeFirstName_Adds_CustomerFirstNameChangedEvent_Domain_Event_On_Success()
    {
        var customer = GetCustomer();
        
        var exception = Record.Exception(() => customer.ChangeFirstName("Tom"));
        
        exception.ShouldBeNull();
        customer.Events.Count().ShouldBe(1);
        var @event = customer.Events.FirstOrDefault() as CustomerFirstNameChangedEvent;
        @event.ShouldNotBeNull();
        @event.CustomerFirstName.Value.ShouldBe("Tom");
    }

    [Fact]
    public void ChangeLastName_Throws_CustomerNotActiveException_When_Customer_Is_Deactivated()
    {
        var customer = GetCustomer();
        customer.Deactivate();
        
        var exception = Record.Exception(() => customer.ChangeLastName("Testsson"));
        
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<CustomerNotActiveException>();
    }
    
    [Fact]
    public void ChangeLastName_Adds_CustomerLastNameChangedEvent_Domain_Event_On_Success()
    {
        var customer = GetCustomer();
        
        var exception = Record.Exception(() => customer.ChangeLastName("Testsson"));
        
        exception.ShouldBeNull();
        customer.Events.Count().ShouldBe(1);
        var @event = customer.Events.FirstOrDefault() as CustomerLastNameChangedEvent;
        @event.ShouldNotBeNull();
        @event.LastName.Value.ShouldBe("Testsson");
    }
    
    [Fact]
    public void Deactivate_Throws_CustomerAlreadyDeactivatedException_When_Customer_Is_Deactivated()
    {
        var customer = GetCustomer();
        customer.Deactivate();
        
        var exception = Record.Exception(() => customer.Deactivate());
        
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<CustomerAlreadyDeactivatedException>();
    }
    
    [Fact]
    public void Deactivate_Adds_CustomerDeactivatedEvent_Domain_Event_On_Success()
    {
        var customer = GetCustomer();
        
        var exception = Record.Exception(() => customer.Deactivate());
        
        exception.ShouldBeNull();
        customer.Events.Count().ShouldBe(1);
        var @event = customer.Events.FirstOrDefault() as CustomerDeactivatedEvent;
        @event.ShouldNotBeNull();
    }

    
    
    #region ARRANGE
    
    private Customer GetCustomer()
    {
        var customer = _customerFactory
            .Create(Guid.NewGuid(), "firebase-uid-mock", "John", "Doe", "john.doe@mock.com");

        customer.ClearEvents();
        return customer;
    }
    
    private readonly ICustomerFactory _customerFactory;

    public CustomerTests()
    {
        _customerFactory = new CustomerFactory();
    }

    #endregion
}