using Floristo.Application.Exceptions;
using Floristo.Domain.Repositories;
using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Commands.Handlers;

public class DeactivateCustomerHandler : ICommandHandler<DeactivateCustomer>
{
    private readonly ICustomerRepository _customerRepository;

    public DeactivateCustomerHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task HandleAsync(DeactivateCustomer command)
    {
        var customer = await _customerRepository.GetAsync(command.Id);

        if (customer is null)
        {
            throw new CustomerNotFoundException(command.Id);
        }
        
        customer.Deactivate();
        
        await _customerRepository.UpdateAsync(customer);
    }
}