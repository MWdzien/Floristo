using Floristo.Application.Categories.Exceptions;
using Floristo.Application.Categories.Services;
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
    private readonly ICategoryReadService _categoryReadService;

    public DeleteCategoryHandler(ICategoryRepository categoryRepository, IProductReadService productReadService, ICategoryReadService categoryReadService)
    {
        _categoryRepository = categoryRepository;
        _productReadService = productReadService;
        _categoryReadService = categoryReadService;
    }

    public async Task HandleAsync(DeleteCategory command)
    {
        var category = await _categoryRepository.GetAsync(command.Id);

        if (category is null)
            throw new CategoryNotFoundException(command.Id);

        if (await _productReadService.ExistsByCategoryAsync(command.Id))
            throw new CategoryHasProductsException(command.Id);

        if (await _categoryReadService.HasChildrenAsync(command.Id))
            throw new CategoryHasChildrenException(command.Id);
        
        category.Delete();
        
        await _categoryRepository.DeleteAsync(category);
    }
}