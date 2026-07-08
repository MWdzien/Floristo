using Floristo.Application.Auth;
using Floristo.Application.Customers.Exceptions;
using Floristo.Domain.Customers.Repositories;
using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Customers.Commands.Handlers;

internal sealed class ChangeCustomerFirstNameHandler : ICommandHandler<ChangeCustomerFirstName>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICurrentUser _currentUser;

    public ChangeCustomerFirstNameHandler(ICustomerRepository customerRepository, ICurrentUser currentUser)
    {
        _customerRepository = customerRepository;
        _currentUser = currentUser;
    }

    public async Task HandleAsync(ChangeCustomerFirstName command)
    {
        var customer = await _customerRepository.GetAsync(_currentUser.Id);

        if (customer is null)
        {
            throw new CustomerNotFoundException(_currentUser.Id);
        }
        
        customer.ChangeFirstName(command.FirstName);
        
        await _customerRepository.UpdateAsync(customer);
    }
}