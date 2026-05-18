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
    public async Task<ActionResult<CategoryDto>> GetCategory([FromRoute] GetCategory query)
    {
        var result = await QueryDispatcher.DispatchAsync(query);
        return OkOrNotFound(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories([FromQuery] GetCategories query)
    {
        var result = await QueryDispatcher.DispatchAsync(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> PostCategory([FromBody] CreateCategory command)
    {
        await CommandDispatcher.DispatchAsync(command);
        return CreatedAtAction(nameof(GetCategory), new { id = command.Id }, null);
    }

    [HttpPut("{id:guid}/name")]
    public async Task<IActionResult> PutName([FromBody] ChangeCategoryName command)
    {
        await CommandDispatcher.DispatchAsync(command);
        return NoContent();
    }
    
    [HttpPut("{id:guid}/slug")]
    public async Task<IActionResult> PutSlug([FromBody] ChangeCategorySlug command)
    {
        await CommandDispatcher.DispatchAsync(command);
        return NoContent();
    }
    
    [HttpPut("{id:guid}/sort-order")]
    public async Task<IActionResult> PutSortOrder([FromBody] ChangeCategorySortOrder command)
    {
        await CommandDispatcher.DispatchAsync(command);
        return NoContent();
    }
    
    [HttpPut("{id:guid}/details")]
    public async Task<IActionResult> PutDetails([FromBody] UpdateCategoryDetails command)
    {
        await CommandDispatcher.DispatchAsync(command);
        return NoContent();
    }
    
    [HttpPut("{id:guid}/publish")]
    public async Task<IActionResult> PutPublish([FromRoute] PublishCategory command)
    {
        await CommandDispatcher.DispatchAsync(command);
        return NoContent();
    }
    
    [HttpPut("{id:guid}/unpublish")]
    public async Task<IActionResult> PutUnpublish([FromRoute] UnpublishCategory command)
    {
        await CommandDispatcher.DispatchAsync(command);
        return NoContent();
    }
    
    [HttpDelete("{id:guid}/description")]
    public async Task<IActionResult> DeleteDescription([FromRoute] DeleteCategoryDescription command)
    {
        await CommandDispatcher.DispatchAsync(command);
        return NoContent();
    }
    
    [HttpDelete("{id:guid}/image-url")]
    public async Task<IActionResult> DeleteImageUrl([FromRoute] DeleteCategoryImageUrl command)
    {
        await CommandDispatcher.DispatchAsync(command);
        return NoContent();
    }
    
    [HttpDelete("{id:guid}/seo-title")]
    public async Task<IActionResult> DeleteSeoTitle([FromRoute] DeleteCategorySeoTitle command)
    {
        await CommandDispatcher.DispatchAsync(command);
        return NoContent();
    }
    
    [HttpDelete("{id:guid}/seo-description")]
    public async Task<IActionResult> DeleteSeoDescription([FromRoute] DeleteCategorySeoDescription command)
    {
        await CommandDispatcher.DispatchAsync(command);
        return NoContent();
    }
    
    
}