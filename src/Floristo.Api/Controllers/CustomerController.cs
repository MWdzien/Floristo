using Floristo.Application.Customers.Commands;
using Floristo.Application.Customers.DTOs;
using Floristo.Application.Customers.Queries;
using Floristo.SharedAbstractions.Commands;
using Floristo.SharedAbstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Floristo.Api.Controllers;

[Route("api/customers")]
public class CustomerController : BaseController
{
    public CustomerController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher) 
        : base(queryDispatcher, commandDispatcher)
    {
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CustomerDto>> GetCustomer([FromRoute] GetCustomer query)
    {
        var result = await QueryDispatcher.DispatchAsync(query);
        return OkOrNotFound(result);
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers([FromQuery] GetCustomers query)
    {
        var result = await QueryDispatcher.DispatchAsync(query);
        return OkOrNotFound(result);
    }

    [HttpPost]
    public async Task<IActionResult> PostCustomer([FromBody] CreateCustomer command)
    {
        await CommandDispatcher.DispatchAsync(command);
        return CreatedAtAction(nameof(GetCustomer), new { id = command.Id }, null);
    }
    
    [HttpPut("{id:guid}/first-name")]
    public async Task<IActionResult> PutFirstName([FromBody] ChangeCustomerFirstName command)
    {
        await CommandDispatcher.DispatchAsync(command);
        return NoContent();
    }
    
    [HttpPut("{id:guid}/last-name")]
    public async Task<IActionResult> PutLastName([FromBody] ChangeCustomerLastName command)
    {
        await CommandDispatcher.DispatchAsync(command);
        return NoContent();
    }
    
    [HttpPut("{id:guid}/email")]
    public async Task<IActionResult> PutEmail([FromBody] ChangeCustomerEmail command)
    {
        await CommandDispatcher.DispatchAsync(command);
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCustomer([FromRoute] DeactivateCustomer command)
    {
        await CommandDispatcher.DispatchAsync(command);
        return NoContent();
    }
}