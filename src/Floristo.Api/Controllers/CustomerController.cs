using Floristo.Application.Commands;
using Floristo.Application.DTOs;
using Floristo.Application.Queries;
using Floristo.SharedAbstractions.Commands;
using Floristo.SharedAbstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Floristo.Api.Controllers;

public class CustomerController : BaseController
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ICommandDispatcher _commandDispatcher;

    public CustomerController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CustomerDto>> Get([FromRoute] GetCustomer query)
    {
        var result = await _queryDispatcher.DispatchAsync(query);
        return OkOrNotFound(result);
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerDto>>> Get([FromQuery] GetCustomers query)
    {
        var result = await _queryDispatcher.DispatchAsync(query);
        return OkOrNotFound(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateCustomer command)
    {
        await _commandDispatcher.DispatchAsync(command);
        return CreatedAtAction(nameof(Get), new { id = command.Id }, null);
    }
    
    [HttpPut("{id:guid}/first-name")]
    public async Task<IActionResult> Put([FromBody] ChangeCustomerFirstName command)
    {
        await _commandDispatcher.DispatchAsync(command);
        return NoContent();
    }
    
    [HttpPut("{id:guid}/last-name")]
    public async Task<IActionResult> Put([FromBody] ChangeCustomerLastName command)
    {
        await _commandDispatcher.DispatchAsync(command);
        return NoContent();
    }
    
    [HttpPut("{id:guid}/email")]
    public async Task<IActionResult> Put([FromBody] ChangeCustomerEmail command)
    {
        await _commandDispatcher.DispatchAsync(command);
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] DeactivateCustomer command)
    {
        await _commandDispatcher.DispatchAsync(command);
        return NoContent();
    }
}