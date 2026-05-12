using Floristo.Application.Commands;
using Floristo.Application.Commands.Handlers;
using Floristo.Application.Exceptions;
using Floristo.Application.Services;
using Floristo.Domain.Factories;
using Floristo.Domain.Repositories;
using Floristo.SharedAbstractions.Commands;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Floristo.Unit.Tests.Application.CustomerTests;

public class ChangeCustomerEmailHandlerTests
{
    Task Act(ChangeCustomerEmail command)
        => _commandHandler.HandleAsync(command);

    [Fact]
    public async Task HandleAsync_Throws_CustomerNotFoundException_When_Customer_Does_Not_Exist()
    {
        var command = new ChangeCustomerEmail(Guid.NewGuid(), "john.doe@mock.com"); 
        
        var exception = await Record.ExceptionAsync(async () => await Act(command));
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<CustomerNotFoundException>();
    }
    
    [Fact]
    public async Task HandleAsync_Throws_CustomerEmailNotUniqueException_When_Email_Is_Not_Unique()
    {
        var command = new ChangeCustomerEmail(Guid.NewGuid(), "john.doe@mock.com");
        var customer = new CustomerFactory().Create(command.Id, "John", "Doe", "old@mock.com");
        _repository.GetAsync(command.Id).Returns(customer);
        _readService.ExistsByEmailAsync(command.Email).Returns(true);
        
        
        var exception = await Record.ExceptionAsync(async () => await Act(command));
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<CustomerEmailNotUniqueException>();
    }
    
    [Fact]
    public async Task HandleAsync_Calls_Repository_On_Success()
    {
        var command = new ChangeCustomerEmail(Guid.NewGuid(), "john.doe@mock.com");
        var customer = new CustomerFactory().Create(command.Id, "John", "Doe", "old@mock.com");
        _repository.GetAsync(command.Id).Returns(customer);
        _readService.ExistsByEmailAsync(command.Email).Returns(false);
        
        var exception = await Record.ExceptionAsync(async () => await Act(command));
        exception.ShouldBeNull();
        await _repository.Received(1).UpdateAsync(customer);
    }
    
    #region ARRANGE
    
    private readonly ICommandHandler<ChangeCustomerEmail> _commandHandler;
    private readonly ICustomerRepository _repository;
    private readonly ICustomerReadService _readService;

    public ChangeCustomerEmailHandlerTests()
    {
        _repository = Substitute.For<ICustomerRepository>();
        _readService = Substitute.For<ICustomerReadService>();
        
        _commandHandler = new ChangeCustomerEmailHandler(_repository, _readService);
    }

    #endregion
}