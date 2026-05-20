using Floristo.Application.Categories.DTOs;
using Floristo.Application.Categories.Queries;
using Floristo.SharedAbstractions.Commands;
using Floristo.SharedAbstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Floristo.Api.Controllers;

[Route("api/storefront/categories")]
public class StorefrontCategoryController : BaseController
{
    public StorefrontCategoryController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        : base(queryDispatcher, commandDispatcher)
    {
    }
    
    [HttpGet("navigation")]
    public async Task<ActionResult<IEnumerable<CategoryNavigationItemDto>>> GetNavigation()
    {
        var query = new GetCategoryNavigation();
        var result = await QueryDispatcher.DispatchAsync(query);
        return Ok(result);
    }

    [HttpGet("{slug}")]
    public async Task<ActionResult<StorefrontCategoryDetailsDto?>> GetCategory([FromRoute] GetCategoryBySlug query)
    {
        var result = await QueryDispatcher.DispatchAsync(query);
        return OkOrNotFound(result);
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StorefrontCategoryDto>>> GetCategories()
    {
        var query = new BrowseCategories();
        var result = await QueryDispatcher.DispatchAsync(query);
        return Ok(result);
    }
}