using Floristo.Application.Exceptions;
using Floristo.Application.Services;
using Floristo.Domain.Repositories;
using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Commands.Handlers;

public class ChangeCustomerEmailHandler : ICommandHandler<ChangeCustomerEmail>
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

        if (await _customerReadService.ExistsByEmailAsync(command.Email))
        {
            throw new CustomerAlreadyExistsException(command.Email);
        }
        
        customer.ChangeEmail(command.Email);
        
        await _customerRepository.UpdateAsync(customer);
    }
}