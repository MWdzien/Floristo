using Floristo.Application.Customers.Exceptions;
using Floristo.Application.Customers.Services;
using Floristo.Domain.Customers.Repositories;
using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Customers.Commands.Handlers;

internal sealed class ChangeCustomerEmailHandler : ICommandHandler<ChangeCustomerEmail>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICustomerReadService _customerReadService;

    public ChangeCustomerEmailHandler(ICustomerRepository customerRepository, ICustomerReadService customerReadService)
    {
        _customerRepository = customerRepository;
        _customerReadService = customerReadService;
    }

    public async Task HandleAsync(ChangeCustomerEmail command)
    {
        var customer = await _customerRepository.GetAsync(command.Id);

        if (customer is null)
        {
            throw new CustomerNotFoundException(command.Id);
        }

        if (await _customerReadService.ExistsByEmailAsync(command.Email, command.Id))
        {
            throw new CustomerEmailNotUniqueException(command.Email);
        }
        
        customer.ChangeEmail(command.Email);
        
        await _customerRepository.UpdateAsync(customer);
    }
}