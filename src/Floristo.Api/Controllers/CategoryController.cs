using Floristo.Application.Categories.Commands;
using Floristo.Application.Categories.DTOs;
using Floristo.Application.Categories.Queries;
using Floristo.SharedAbstractions.Commands;
using Floristo.SharedAbstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Floristo.Api.Controllers;

[Route("api/categories")]
public class CategoryController : BaseController
{
    public CategoryController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher) 
        : base(queryDispatcher, commandDispatcher)
    {
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CategoryDto>> Get([FromRoute] GetCategory query)
    {
        var result = await QueryDispatcher.DispatchAsync(query);
        return OkOrNotFound(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> Get([FromQuery] GetCategories query)
    {
        var result = await QueryDispatcher.DispatchAsync(query);
        return OkOrNotFound(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateCategory command)
    {
        await CommandDispatcher.DispatchAsync(command);
        return CreatedAtAction(nameof(Get), new { id = command.Id }, null);
    }

    [HttpPut("{id:guid}/name")]
    public async Task<IActionResult> Put([FromBody] ChangeCategoryName command)
    {
        await CommandDispatcher.DispatchAsync(command);
        return NoContent();
    }
    
    [HttpPut("{id:guid}/slug")]
    public async Task<IActionResult> Put([FromBody] ChangeCategorySlug command)
    {
        await CommandDispatcher.DispatchAsync(command);
        return NoContent();
    }
    
    [HttpPut("{id:guid}/sort-order")]
    public async Task<IActionResult> Put([FromBody] ChangeCategorySortOrder command)
    {
        await CommandDispatcher.DispatchAsync(command);
        return NoContent();
    }
    
    [HttpPut("{id:guid}/details")]
    public async Task<IActionResult> Put([FromBody] UpdateCategoryDetails command)
    {
        await CommandDispatcher.DispatchAsync(command);
        return NoContent();
    }
    
    [HttpPut("{id:guid}/activate")]
    public async Task<IActionResult> Put([FromRoute] ActivateCategory command)
    {
        await CommandDispatcher.DispatchAsync(command);
        return NoContent();
    }
    
    [HttpPut("{id:guid}/deactivate")]
    public async Task<IActionResult> Put([FromRoute] DeactivateCategory command)
    {
        await CommandDispatcher.DispatchAsync(command);
        return NoContent();
    }
    
    [HttpDelete("{id:guid}/description")]
    public async Task<IActionResult> Delete([FromRoute] DeleteCategoryDescription command)
    {
        await CommandDispatcher.DispatchAsync(command);
        return NoContent();
    }
    
    [HttpDelete("{id:guid}/image-url")]
    public async Task<IActionResult> Delete([FromRoute] DeleteCategoryImageUrl command)
    {
        await CommandDispatcher.DispatchAsync(command);
        return NoContent();
    }
    
    [HttpDelete("{id:guid}/seo-title")]
    public async Task<IActionResult> Delete([FromRoute] DeleteCategorySeoTitle command)
    {
        await CommandDispatcher.DispatchAsync(command);
        return NoContent();
    }
    
    [HttpDelete("{id:guid}/seo-description")]
    public async Task<IActionResult> Delete([FromRoute] DeleteCategorySeoDescription command)
    {
        await CommandDispatcher.DispatchAsync(command);
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] DeleteCategory command)
    {
        await CommandDispatcher.DispatchAsync(command);
        return NoContent();
    }
    
    
}