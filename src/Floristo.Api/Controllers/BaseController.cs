using Microsoft.AspNetCore.Mvc;

namespace Floristo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected ActionResult<TResult> OkOrNotFound<TResult>(TResult result)
        => result == null ? NotFound() : Ok(result);
}