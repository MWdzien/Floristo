using Floristo.Application.Customers.Commands;
using Floristo.Application.Customers.DTOs;
using Floristo.Application.Customers.Queries;
using Floristo.SharedAbstractions.Commands;
using Floristo.SharedAbstractions.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Floristo.Api.Controllers;

[Authorize(Roles = "admin")]
[Route("api/backoffice/customers")]
public class BackofficeCustomerController : BaseController
{
    public BackofficeCustomerController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher) 
        : base(queryDispatcher, commandDispatcher)
    {
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<BackofficeCustomerDetailsDto>> GetCustomer([FromRoute] GetCustomerForBackoffice query)
    {
        var result = await QueryDispatcher.DispatchAsync(query);
        return OkOrNotFound(result);
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BackofficeCustomerDto>>> GetCustomers([FromQuery] GetCustomersForBackoffice query)
    {
        var result = await QueryDispatcher.DispatchAsync(query);
        return Ok(result);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCustomer([FromRoute] Guid id)
    {
        await CommandDispatcher.DispatchAsync(new DeactivateCustomer(id));
        return NoContent();
    }
}