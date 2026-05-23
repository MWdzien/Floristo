using Floristo.Application.Categories.Exceptions;
using Floristo.Application.Products.Exceptions;
using Floristo.Application.Products.Services;
using Floristo.Domain.Categories.Repositories;
using Floristo.Domain.Products.Repositories;
using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Categories.Commands.Handlers;

internal sealed class DeleteCategoryHandler : ICommandHandler<DeleteCategory>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductReadService _productReadService;

    public DeleteCategoryHandler(ICategoryRepository categoryRepository, IProductReadService productReadService)
    {
        _categoryRepository = categoryRepository;
        _productReadService = productReadService;
    }

    public async Task HandleAsync(DeleteCategory command)
    {
        var category = await _categoryRepository.GetAsync(command.Id);

        if (category is null)
            throw new CategoryNotFoundException(command.Id);

        if (await _productReadService.HasProductsInCategoryAsync(command.Id))
            throw new CategoryHasProductsException();
        
        category.Delete();
        
        await _categoryRepository.DeleteAsync(category);
    }
}