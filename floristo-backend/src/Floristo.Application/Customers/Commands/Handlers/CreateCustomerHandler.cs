using Floristo.Application.Auth;
using Floristo.Application.Customers.Exceptions;
using Floristo.Application.Customers.Services;
using Floristo.Domain.Customers.Exceptions;
using Floristo.Domain.Customers.Factories;
using Floristo.Domain.Customers.Repositories;
using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Customers.Commands.Handlers;

internal sealed class CreateCustomerHandler : ICommandHandler<CreateCustomer>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICustomerFactory _customerFactory;
    private readonly ICustomerReadService _customerReadService;
    private readonly ICurrentUser _currentUser;

    public CreateCustomerHandler(ICustomerRepository customerRepository, ICustomerFactory customerFactory, 
        ICustomerReadService customerReadService, ICurrentUser currentUser)
    {
        _customerRepository = customerRepository;
        _customerFactory = customerFactory;
        _customerReadService = customerReadService;
        _currentUser = currentUser;
    }

    public async Task HandleAsync(CreateCustomer command)
    {
        var id = _currentUser.Id;
        var email = _currentUser.Email ??
                    throw new CustomerEmailMissingException();
        
        if (await _customerReadService.ExistsByIdAsync(id))
        {
            throw new CustomerAlreadyExistsException(id);
        }
        
        var customer = _customerFactory.Create(id, command.FirstName, command.LastName, email);
        
        await _customerRepository.AddAsync(customer);
    }
}