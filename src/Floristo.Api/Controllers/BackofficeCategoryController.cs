using Floristo.Application.Categories.Commands;
using Floristo.Application.Categories.DTOs;
using Floristo.Application.Categories.Queries;
using Floristo.SharedAbstractions.Commands;
using Floristo.SharedAbstractions.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Floristo.Api.Controllers;

[Authorize(Roles = "admin")]
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
        command = command with { Id = Guid.NewGuid() };
        await CommandDispatcher.DispatchAsync(command);
        return CreatedAtAction(nameof(GetCategory), new { id = command.Id }, null);
    }

    [HttpPut("{id:guid}/sort-order")]
    public async Task<IActionResult> PutSortOrder(Guid id, [FromBody] ChangeCategorySortOrder command)
    {
        await CommandDispatcher.DispatchAsync(command with { Id = id });
        return NoContent();
    }
    
    [HttpPut("{id:guid}/details")]
    public async Task<IActionResult> PutDetails(Guid id, [FromBody] UpdateCategoryDetails command)
    {
        await CommandDispatcher.DispatchAsync(command with { Id = id });
        return NoContent();
    }
    
    [HttpPut("{id:guid}/publish")]
    public async Task<IActionResult> PutPublish(Guid id, [FromBody] PublishCategory command)
    {
        await CommandDispatcher.DispatchAsync(command with { Id = id });
        return NoContent();
    }
    
    [HttpPut("{id:guid}/unpublish")]
    public async Task<IActionResult> PutUnpublish(Guid id, [FromRoute] UnpublishCategory command)
    {
        await CommandDispatcher.DispatchAsync(command with { Id = id });
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCategory(Guid id, [FromRoute] DeleteCategory command)
    {
        await CommandDispatcher.DispatchAsync(command with { Id = id});
        return NoContent();
    }
    
}