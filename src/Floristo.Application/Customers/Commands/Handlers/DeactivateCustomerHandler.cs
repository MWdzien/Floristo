using Floristo.Application.Auth;
using Floristo.Application.Customers.Exceptions;
using Floristo.Domain.Customers.Repositories;
using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Customers.Commands.Handlers;

internal sealed class DeactivateCustomerHandler : ICommandHandler<DeactivateCustomer>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICurrentUser _currentUser;

    public DeactivateCustomerHandler(ICustomerRepository customerRepository, ICurrentUser currentUser)
    {
        _customerRepository = customerRepository;
        _currentUser = currentUser;
    }

    public async Task HandleAsync(DeactivateCustomer command)
    {
        command = command with { Id = _currentUser.Id };
        
        var customer = await _customerRepository.GetAsync(command.Id);

        if (customer is null)
        {
            throw new CustomerNotFoundException(command.Id);
        }
        
        customer.Deactivate();
        
        await _customerRepository.UpdateAsync(customer);
    }
}