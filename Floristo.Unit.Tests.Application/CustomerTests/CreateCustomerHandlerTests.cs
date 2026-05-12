using Floristo.Application.Commands;
using Floristo.Application.Commands.Handlers;
using Floristo.Application.Exceptions;
using Floristo.Application.Services;
using Floristo.Domain.Entities;
using Floristo.Domain.Factories;
using Floristo.Domain.Repositories;
using Floristo.SharedAbstractions.Commands;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Floristo.Unit.Tests.Application.CustomerTests;

public class CreateCustomerHandlerTests
{
    Task Act(CreateCustomer command)
        => _commandHandler.HandleAsync(command);

    [Fact]
    public async Task HandleAsync_Throws_CustomerAlreadyExistsException_When_Customer_With_Same_Email_Exists()
    {
        var command = GetCreateCustomer();
        _readService.ExistsByEmailAsync(command.Email).Returns(true);
        
        var exception = await Record.ExceptionAsync(() => Act(command));
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<CustomerEmailNotUniqueException>();
    }
    
    [Fact]
    public async Task HandleAsync_Calls_Repository_On_Success()
    {
        var command = GetCreateCustomer();
        _readService.ExistsByEmailAsync(command.Email).Returns(false);
        _factory.Create(command.Id, command.FirstName, command.LastName, command.Email).Returns(default(Customer));
        
        var exception = await Record.ExceptionAsync(() => Act(command));
        exception.ShouldBeNull();
        await _repository.Received(1).AddAsync(Arg.Any<Customer>());
    }
    
    #region ARRANGE

    public CreateCustomer GetCreateCustomer()
    {
        return new CreateCustomer(Guid.NewGuid(), "John", "Doe", "john.doe@mock.com");
    }
    
    private readonly ICommandHandler<CreateCustomer> _commandHandler;
    private readonly ICustomerRepository _repository;
    private readonly ICustomerReadService _readService;
    private readonly ICustomerFactory _factory;

    public CreateCustomerHandlerTests()
    {
        _repository = Substitute.For<ICustomerRepository>();
        _factory = Substitute.For<ICustomerFactory>();
        _readService = Substitute.For<ICustomerReadService>();
        
        _commandHandler = new CreateCustomerHandler(_repository, _factory, _readService);
    }

    #endregion
}