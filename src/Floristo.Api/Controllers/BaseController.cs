using Floristo.SharedAbstractions.Commands;
using Floristo.SharedAbstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Floristo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected readonly IQueryDispatcher QueryDispatcher;
    protected readonly ICommandDispatcher CommandDispatcher;

    protected BaseController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
    {
        QueryDispatcher = queryDispatcher;
        CommandDispatcher = commandDispatcher;
    }

    protected ActionResult<TResult> OkOrNotFound<TResult>(TResult result)
        => result == null ? NotFound() : Ok(result);
}