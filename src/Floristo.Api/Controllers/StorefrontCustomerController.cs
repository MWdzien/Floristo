using Floristo.Application.Customers.Commands;
using Floristo.Application.Customers.DTOs;
using Floristo.Application.Customers.Queries;
using Floristo.SharedAbstractions.Commands;
using Floristo.SharedAbstractions.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Floristo.Api.Controllers;

[Authorize]
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
        await CommandDispatcher.DispatchAsync(command);
        return Created();
    }
    
    [HttpPut("first-name")]
    public async Task<IActionResult> PutFirstName([FromBody] ChangeCustomerFirstName command)
    {
        await CommandDispatcher.DispatchAsync(command);
        return NoContent();
    }
    
    [HttpPut("last-name")]
    public async Task<IActionResult> PutLastName([FromBody] ChangeCustomerLastName command)
    {
        await CommandDispatcher.DispatchAsync(command);
        return NoContent();
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteCustomer([FromQuery] DeactivateCustomer command)
    {
        await CommandDispatcher.DispatchAsync(command);
        return NoContent();
    }
}