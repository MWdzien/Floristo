using Floristo.Application.Products.Consts;
using Floristo.Application.Products.DTOs;
using Floristo.Application.Products.Queries;
using Floristo.SharedAbstractions.Commands;
using Floristo.SharedAbstractions.Paging;
using Floristo.SharedAbstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Floristo.Api.Controllers;

[Route("api/storefront/products")]
public class StorefrontProductController : BaseController
{
    public StorefrontProductController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher) : base(queryDispatcher, commandDispatcher)
    {
    }
    
    [HttpGet("search")]
    public async Task<ActionResult<PagedResult<StorefrontProductDto>>> SearchProducts([FromQuery] SearchProducts query)
    {                                                                                                                                                     
        var result = await QueryDispatcher.DispatchAsync(query);                                                                                                                                                           
        return Ok(result);
    }
    
    [HttpGet("new-arrivals")]
    public async Task<ActionResult<IEnumerable<StorefrontProductDto>>> GetNewArrivals([FromQuery] GetNewArrivals query)
    {
        var result = await QueryDispatcher.DispatchAsync(query);
        return Ok(result);
    }
    
    [HttpGet("category/{categorySlug}")]
    public async Task<ActionResult<PagedResult<StorefrontProductDto>>> BrowseProductsByCategory(string categorySlug, [FromQuery] BrowseProductsByCategory query)
    {
        var result = await QueryDispatcher.DispatchAsync(query with { CategorySlug = categorySlug });
        return Ok(result);
    }
    
    [HttpGet("{slug}")]
    public async Task<ActionResult<StorefrontProductDetailsDto?>> GetProduct([FromRoute] GetProductBySlug query)
    {
        var result = await QueryDispatcher.DispatchAsync(query);
        return OkOrNotFound(result);
    }
    
    [HttpGet("{slug}/related")]
    public async Task<ActionResult<IEnumerable<StorefrontProductDto>>> GetRelatedProducts(string slug, [FromQuery] GetRelatedProducts query)
    {
        var result = await QueryDispatcher.DispatchAsync(query with { Slug = slug });
        return Ok(result);
    }
}