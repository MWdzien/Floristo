using Floristo.Application.Categories.Commands;
using Floristo.Application.Categories.DTOs;
using Floristo.Application.Categories.Queries;
using Floristo.SharedAbstractions.Commands;
using Floristo.SharedAbstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Floristo.Api.Controllers;

[Route("api/backoffice/categories")]
public class BackofficeCategoryController : BaseController
{
    public BackofficeCategoryController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        : base(queryDispatcher, commandDispatcher)
    {
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<BackofficeCategoryDetailsDto?>> GetCategory([FromRoute] GetCategoryForBackoffice query)
    {
        var result = await QueryDispatcher.DispatchAsync(query);
        return OkOrNotFound(result);
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BackofficeCategoryDto>>> GetCategories([FromQuery] GetCategoriesForBackoffice query)
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
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] DeleteCategory command)
    {
        await CommandDispatcher.DispatchAsync(command);
        return NoContent();
    }
    
    
}