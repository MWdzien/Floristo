using Floristo.Application.Customers.Exceptions;
using Floristo.Application.Customers.Services;
using Floristo.Domain.Customers.Factories;
using Floristo.Domain.Customers.Repositories;
using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Customers.Commands.Handlers;

internal sealed class CreateCustomerHandler : ICommandHandler<CreateCustomer>
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
            throw new CustomerEmailNotUniqueException(email);
        }
        
        var customer = _customerFactory.Create(id, firstName, lastName, email);
        
        await _customerRepository.AddAsync(customer);
    }
}