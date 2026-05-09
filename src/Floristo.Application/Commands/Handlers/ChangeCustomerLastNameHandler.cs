using Floristo.Application.Exceptions;
using Floristo.Domain.Repositories;
using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Commands.Handlers;

public class ChangeCustomerLastNameHandler : ICommandHandler<ChangeCustomerLastName>
{
    private readonly ICustomerRepository _customerRepository;

    public ChangeCustomerLastNameHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task HandleAsync(ChangeCustomerLastName command)
    {
        var customer = await _customerRepository.GetAsync(command.Id);

        if (customer is null)
        {
            throw new CustomerNotFoundException(command.Id);
        }
        
        customer.ChangeLastName(command.LastName);
        
        await _customerRepository.UpdateAsync(customer);
    }
}