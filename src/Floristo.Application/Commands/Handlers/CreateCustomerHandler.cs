using Floristo.Application.Exceptions;
using Floristo.Application.Services;
using Floristo.Domain.Factories;
using Floristo.Domain.Repositories;
using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Commands.Handlers;

public class CreateCustomerHandler : ICommandHandler<CreateCustomer>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICustomerFactory _customerFactory;
    private readonly ICustomerReadService _customerReadService;

    public CreateCustomerHandler(ICustomerRepository customerRepository, ICustomerFactory customerFactory, 
        ICustomerReadService customerReadService)
    {
        _customerRepository = customerRepository;
        _customerFactory = customerFactory;
        _customerReadService = customerReadService;
    }

    public async Task HandleAsync(CreateCustomer command)
    {
        var (id, firstName, lastName, email) = command;
        
        if (await _customerReadService.ExistsByEmailAsync(email))
        {
            throw new CustomerAlreadyExistsException(email);
        }
        
        var customer = _customerFactory.Create(id, firstName, lastName, email);
        
        await _customerRepository.AddAsync(customer);
    }
}