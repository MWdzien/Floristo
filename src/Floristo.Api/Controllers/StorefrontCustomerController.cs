using Floristo.Application.Customers.Commands;
using Floristo.Application.Customers.DTOs;
using Floristo.Application.Customers.Queries;
using Floristo.SharedAbstractions.Commands;
using Floristo.SharedAbstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Floristo.Api.Controllers;

[Route("api/storefront/account")]
public class StorefrontCustomerController : BaseController
{
    public StorefrontCustomerController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher) : base(queryDispatcher, commandDispatcher)
    {
    }

    [HttpGet]
    public async Task<ActionResult<StorefrontCustomerDto>> GetCustomer([FromQuery] GetCurrentCustomer query)
    {
        var result = await QueryDispatcher.DispatchAsync(query);
        return OkOrNotFound(result);
    } 
    
    [HttpPost]
    public async Task<IActionResult> PostCustomer([FromBody] CreateCustomer command)
    {
        command = command with { Id = Guid.NewGuid() };
        await CommandDispatcher.DispatchAsync(command);
        return CreatedAtAction(nameof(GetCustomer), new { id = command.Id }, null);
    }
    
    [HttpPut("first-name")]
    public async Task<IActionResult> PutFirstName(Guid id, [FromBody] ChangeCustomerFirstName command)
    {
        await CommandDispatcher.DispatchAsync(command with { Id = id });
        return NoContent();
    }
    
    [HttpPut("last-name")]
    public async Task<IActionResult> PutLastName(Guid id, [FromBody] ChangeCustomerLastName command)
    {
        await CommandDispatcher.DispatchAsync(command with { Id = id });
        return NoContent();
    }
    
    [HttpPut("email")]
    public async Task<IActionResult> PutEmail(Guid id, [FromBody] ChangeCustomerEmail command)
    {
        await CommandDispatcher.DispatchAsync(command with { Id = id });
        return NoContent();
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteCustomer([FromQuery] Guid id)
    {
        await CommandDispatcher.DispatchAsync(new DeactivateCustomer(id));
        return NoContent();
    }
}