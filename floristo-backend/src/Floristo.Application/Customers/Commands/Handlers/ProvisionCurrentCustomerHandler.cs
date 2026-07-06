using Floristo.Application.Auth;
using Floristo.Application.Customers.Exceptions;
using Floristo.Application.Customers.Services;
using Floristo.Domain.Customers.Exceptions;
using Floristo.Domain.Customers.Factories;
using Floristo.Domain.Customers.Repositories;
using Floristo.SharedAbstractions.Commands;
using Microsoft.Extensions.Logging;

namespace Floristo.Application.Customers.Commands.Handlers;

internal sealed class ProvisionCurrentCustomerHandler : ICommandHandler<ProvisionCurrentCustomer>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICustomerFactory _customerFactory;
    private readonly ICustomerReadService _customerReadService;
    private readonly ICurrentUser _currentUser;
    private readonly IIdentityCustomerLinker _identityCustomerLinker;
    private readonly ILogger<ProvisionCurrentCustomerHandler> _logger;

    public ProvisionCurrentCustomerHandler(ICustomerRepository customerRepository, ICustomerFactory customerFactory, 
        ICustomerReadService customerReadService, ICurrentUser currentUser, IIdentityCustomerLinker identityCustomerLinker, 
        ILogger<ProvisionCurrentCustomerHandler> logger)
    {
        _customerRepository = customerRepository;
        _customerFactory = customerFactory;
        _customerReadService = customerReadService;
        _currentUser = currentUser;
        _identityCustomerLinker = identityCustomerLinker;
        _logger = logger;
    }

    public async Task HandleAsync(ProvisionCurrentCustomer command)
    {
        var identityId = _currentUser.IdentityId;
        var email = _currentUser.Email ??
                    throw new CustomerEmailMissingException();

        if (await _customerReadService.GetIdByIdentityIdAsync(identityId) != null)
        {
            throw new CustomerAlreadyExistsException(identityId);
        }
        
        var customer = _customerFactory.Create(Guid.NewGuid(), identityId, command.FirstName, command.LastName, email);
        
        await _customerRepository.AddAsync(customer);

        try
        {
            await _identityCustomerLinker.LinkCustomerAsync(identityId, customer.Id);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to link identity {IdentityId} with customer {CustomerId}. " +
                                   "Customer was provisioned but token custom claim was not set — will fallback to DB " +
                                   "lookup on subsequent requests.", identityId, customer.Id);
        }
    }
}