using Floristo.Application.Auth;
using Floristo.Application.Customers.Commands;
using Floristo.Application.Customers.Commands.Handlers;
using Floristo.Application.Customers.Exceptions;
using Floristo.Application.Customers.Services;
using Floristo.Domain.Customers.Entities;
using Floristo.Domain.Customers.Exceptions;
using Floristo.Domain.Customers.Factories;
using Floristo.Domain.Customers.Repositories;
using Floristo.Domain.Customers.ValueObjects;
using Floristo.SharedAbstractions.Commands;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Floristo.Unit.Tests.Application.CustomerTests;

public class ProvisionCurrentCustomerHandlerTests
{
    Task Act(ProvisionCurrentCustomer command)
        => _commandHandler.HandleAsync(command);

    [Fact]
    public async Task HandleAsync_Throws_CustomerAlreadyExistsException_When_Customer_With_Same_Id_Exists()
    {
        var command = GetCreateCustomer();
        _readService.ExistsByIdAsync(_currentUser.Id).Returns(true);
        
        var exception = await Record.ExceptionAsync(() => Act(command));
        
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<CustomerAlreadyExistsException>();
    }

    [Fact]
    public async Task HandleAsync_Throws_CustomerEmailMissingException_When_User_Has_No_Email()
    {
        var command = GetCreateCustomer();
        _currentUser.Email.Returns((string?)null);
        _readService.ExistsByIdAsync(_currentUser.Id).Returns(false);
        
        var exception = await Record.ExceptionAsync(() => Act(command));
        
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<CustomerEmailMissingException>();
    }
    
    [Fact]
    public async Task HandleAsync_Calls_Repository_On_Success()
    {
        var command = GetCreateCustomer();
        _readService.ExistsByIdAsync(_currentUser.Id).Returns(false);
        _factory.Create(Arg.Any<CustomerId>(), Arg.Any<CustomerFirstName>(), Arg.Any<CustomerLastName>(), Arg.Any<CustomerEmail>()).Returns(default(Customer));
        
        var exception = await Record.ExceptionAsync(() => Act(command));
        
        exception.ShouldBeNull();
        await _repository.Received(1).AddAsync(Arg.Any<Customer>());
    }
    
    #region ARRANGE

    public ProvisionCurrentCustomer GetCreateCustomer()
        => new("John", "Doe");
    
    private readonly ICommandHandler<ProvisionCurrentCustomer> _commandHandler;
    private readonly ICustomerRepository _repository;
    private readonly ICustomerReadService _readService;
    private readonly ICustomerFactory _factory;
    private readonly ICurrentUser _currentUser;

    public ProvisionCurrentCustomerHandlerTests()
    {
        _repository = Substitute.For<ICustomerRepository>();
        _factory = Substitute.For<ICustomerFactory>();
        _readService = Substitute.For<ICustomerReadService>();
        _currentUser = Substitute.For<ICurrentUser>();

        _currentUser.Id.Returns(Guid.NewGuid());
        _currentUser.Email.Returns("john.doe@mock.com");
        
        _commandHandler = new ProvisionCurrentCustomerHandler(_repository, _factory, _readService, _currentUser);
    }

    #endregion
}