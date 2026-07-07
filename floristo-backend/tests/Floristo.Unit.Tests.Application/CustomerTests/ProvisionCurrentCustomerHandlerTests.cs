using Floristo.Application.Auth;
using Floristo.Application.Customers.Commands;
using Floristo.Application.Customers.Commands.Handlers;
using Floristo.Application.Customers.Services;
using Floristo.Domain.Customers.Entities;
using Floristo.Domain.Customers.Exceptions;
using Floristo.Domain.Customers.Factories;
using Floristo.Domain.Customers.Repositories;
using Floristo.Domain.Customers.ValueObjects;
using Floristo.SharedAbstractions.Commands;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Floristo.Unit.Tests.Application.CustomerTests;

public class ProvisionCurrentCustomerHandlerTests
{
    Task Act(ProvisionCurrentCustomer command)
        => _commandHandler.HandleAsync(command);

    [Fact]
    public async Task HandleAsync_Throws_CustomerAlreadyExistsException_When_Customer_With_Same_IdentityId_Exists()
    {
        var command = GetProvisionCurrentCustomer();
        _readService.GetIdByIdentityIdAsync(_currentUser.IdentityId).Returns(Guid.NewGuid());

        var exception = await Record.ExceptionAsync(() => Act(command));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<CustomerAlreadyExistsException>();
    }

    [Fact]
    public async Task HandleAsync_Throws_CustomerEmailMissingException_When_User_Has_No_Email()
    {
        var command = GetProvisionCurrentCustomer();
        _currentUser.Email.Returns((string?)null);
        _readService.GetIdByIdentityIdAsync(_currentUser.IdentityId).Returns((Guid?)null);

        var exception = await Record.ExceptionAsync(() => Act(command));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<CustomerEmailMissingException>();
    }

    [Fact]
    public async Task HandleAsync_Calls_Repository_On_Success()
    {
        var command = GetProvisionCurrentCustomer();
        _readService.GetIdByIdentityIdAsync(_currentUser.IdentityId).Returns((Guid?)null);

        var exception = await Record.ExceptionAsync(() => Act(command));

        exception.ShouldBeNull();
        await _repository.Received(1).AddAsync(Arg.Any<Customer>());
    }

    [Fact]
    public async Task HandleAsync_Links_Identity_With_Customer_On_Success()
    {
        var command = GetProvisionCurrentCustomer();
        _readService.GetIdByIdentityIdAsync(_currentUser.IdentityId).Returns((Guid?)null);

        var exception = await Record.ExceptionAsync(() => Act(command));

        exception.ShouldBeNull();
        await _identityCustomerLinker.Received(1).LinkCustomerAsync(_currentUser.IdentityId, Arg.Any<Guid>());
    }

    [Fact]
    public async Task HandleAsync_Does_Not_Throw_When_Identity_Linking_Fails()
    {
        var command = GetProvisionCurrentCustomer();
        _readService.GetIdByIdentityIdAsync(_currentUser.IdentityId).Returns((Guid?)null);
        _identityCustomerLinker.LinkCustomerAsync(Arg.Any<string>(), Arg.Any<Guid>())
            .Returns(Task.FromException(new InvalidOperationException("linker down")));

        var exception = await Record.ExceptionAsync(() => Act(command));

        exception.ShouldBeNull();
    }

    #region ARRANGE

    public ProvisionCurrentCustomer GetProvisionCurrentCustomer()
        => new("John", "Doe");

    private readonly ICommandHandler<ProvisionCurrentCustomer> _commandHandler;
    private readonly ICustomerRepository _repository;
    private readonly ICustomerReadService _readService;
    private readonly ICustomerFactory _factory;
    private readonly ICurrentUser _currentUser;
    private readonly IIdentityCustomerLinker _identityCustomerLinker;

    public ProvisionCurrentCustomerHandlerTests()
    {
        _repository = Substitute.For<ICustomerRepository>();
        _factory = new CustomerFactory();
        _readService = Substitute.For<ICustomerReadService>();
        _currentUser = Substitute.For<ICurrentUser>();
        _identityCustomerLinker = Substitute.For<IIdentityCustomerLinker>();

        _currentUser.Id.Returns(Guid.NewGuid());
        _currentUser.IdentityId.Returns("firebase-uid-mock");
        _currentUser.Email.Returns("john.doe@mock.com");

        _commandHandler = new ProvisionCurrentCustomerHandler(_repository, _factory, _readService, _currentUser,
            _identityCustomerLinker, NullLogger<ProvisionCurrentCustomerHandler>.Instance);
    }

    #endregion
}
