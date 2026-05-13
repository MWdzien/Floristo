using Floristo.Application.Customers.Exceptions;
using Floristo.Domain.Customers.Repositories;
using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Customers.Commands.Handlers;

public class ChangeCustomerFirstNameHandler : ICommandHandler<ChangeCustomerFirstName>
{
    private readonly ICustomerRepository _customerRepository;

    public ChangeCustomerFirstNameHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task HandleAsync(ChangeCustomerFirstName command)
    {
        var customer = await _customerRepository.GetAsync(command.Id);

        if (customer is null)
        {
            throw new CustomerNotFoundException(command.Id);
        }
        
        customer.ChangeFirstName(command.FirstName);
        
        await _customerRepository.UpdateAsync(customer);
    }
}