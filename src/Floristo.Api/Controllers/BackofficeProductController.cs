using Floristo.Application.Products.Commands;
using Floristo.Application.Products.DTOs;
using Floristo.Application.Products.Queries;
using Floristo.SharedAbstractions.Commands;
using Floristo.SharedAbstractions.Paging;
using Floristo.SharedAbstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Floristo.Api.Controllers;

[Route("api/backoffice/products")]
public class BackofficeProductController : BaseController
{
    public BackofficeProductController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher) : base(queryDispatcher, commandDispatcher)
    {
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<BackofficeProductDto>>> BrowseProductsForBackoffice(
        [FromQuery] BrowseProductsForBackoffice query)
    {
        var result = await QueryDispatcher.DispatchAsync(query);
        return Ok(result);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<BackofficeProductDetailsDto>> GetProductForBackoffice(
        [FromRoute] GetProductForBackoffice query)
    {
        var result = await QueryDispatcher.DispatchAsync(query);
        return OkOrNotFound(result);
    }

    [HttpPost]
    public async Task<IActionResult> PostProduct([FromBody] CreateProduct command)
    {
        command = command with { Id = Guid.NewGuid() };
        await CommandDispatcher.DispatchAsync(command);
        return CreatedAtAction(nameof(GetProductForBackoffice), new { id = command.Id }, null);
    }

    [HttpPut("{id:guid}/details")]
    public async Task<IActionResult> PutDetails(Guid id, [FromBody] UpdateProductDetails command)
    {
        await CommandDispatcher.DispatchAsync(command with { Id = id });
        return NoContent();
    }
    
    [HttpPut("{id:guid}/price")]
    public async Task<IActionResult> PutPrice(Guid id, [FromBody] ChangeProductPrice command)
    {
        await CommandDispatcher.DispatchAsync(command with { Id = id });
        return NoContent();
    }
    
    [HttpPut("{id:guid}/sort-order")]
    public async Task<IActionResult> PutSortOrder(Guid id, [FromBody] ChangeProductSortOrder command)
    {
        await CommandDispatcher.DispatchAsync(command with { Id = id });
        return NoContent();
    }
    
    [HttpPut("{id:guid}/category")]
    public async Task<IActionResult> PutCategory(Guid id, [FromBody] ChangeProductCategory command)
    {
        await CommandDispatcher.DispatchAsync(command with { Id = id });
        return NoContent();
    }
    
    [HttpPut("{id:guid}/publish")]
    public async Task<IActionResult> PutPublish(Guid id, [FromBody] PublishProduct command)
    {
        await CommandDispatcher.DispatchAsync(command with { Id = id });
        return NoContent();
    }
    
    [HttpPut("{id:guid}/unpublish")]
    public async Task<IActionResult> PutUnpublish(Guid id, [FromBody] UnpublishProduct command)
    {
        await CommandDispatcher.DispatchAsync(command with { Id = id });
        return NoContent();
    }
    
    [HttpPut("{id:guid}/engraving/allow")]
    public async Task<IActionResult> PutAllowEngraving(Guid id, [FromBody] AllowProductEngraving command)
    {
        await CommandDispatcher.DispatchAsync(command with { Id = id });
        return NoContent();
    }
    
    [HttpPut("{id:guid}/engraving/disallow")]
    public async Task<IActionResult> PutDisallowEngraving(Guid id, [FromBody] DisallowProductEngraving command)
    {
        await CommandDispatcher.DispatchAsync(command with { Id = id });
        return NoContent();
    }
    
    [HttpPut("{id:guid}/gift-tag/allow")]
    public async Task<IActionResult> PutAllowGiftTag(Guid id, [FromBody] AllowProductGiftTag command)
    {
        await CommandDispatcher.DispatchAsync(command with { Id = id });
        return NoContent();
    }
    
    [HttpPut("{id:guid}/gift-tag/disallow")]
    public async Task<IActionResult> PutDisallowGiftTag(Guid id, [FromBody] DisallowProductGiftTag command)
    {
        await CommandDispatcher.DispatchAsync(command with { Id = id });
        return NoContent();
    }

    [HttpPost("{id:guid}/images")]
    public async Task<IActionResult> PostImage(Guid id, [FromBody] AddProductImage command)
    {
        await CommandDispatcher.DispatchAsync(command with { Id = id, ImageId = Guid.NewGuid() });
        return Created();
    }
    
    [HttpPut("{id:guid}/images/{imageId:guid}/sort-order")]
    public async Task<IActionResult> PutImageSortOrder(Guid id, Guid imageId, [FromBody] ChangeProductImageSortOrder command)
    {
        await CommandDispatcher.DispatchAsync(command with { Id = id, ImageId = imageId});
        return NoContent();
    }
    
    [HttpDelete("{id:guid}/images/{imageId:guid}")]
    public async Task<IActionResult> DeleteImage(Guid id, Guid imageId, [FromBody] RemoveProductImage command)
    {
        await CommandDispatcher.DispatchAsync(command with { Id = id, ImageId = imageId });
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProduct(Guid id, [FromRoute] DeleteProduct command)
    {
        await CommandDispatcher.DispatchAsync(command with { Id = id});
        return NoContent();
    }
}