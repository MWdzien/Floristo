using Floristo.Application.Categories.Exceptions;
using Floristo.Application.Categories.Services;
using Floristo.Application.Products.Services;
using Floristo.Domain.Categories.Factories;
using Floristo.Domain.Categories.Repositories;
using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Categories.Commands.Handlers;

internal sealed class CreateCategoryHandler : ICommandHandler<CreateCategory>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryFactory _categoryFactory;
    private readonly ICategoryReadService _categoryReadService;
    private readonly IProductReadService _productReadService;

    public CreateCategoryHandler(ICategoryRepository categoryRepository, ICategoryFactory categoryFactory, ICategoryReadService categoryReadService, IProductReadService productReadService)
    {
        _categoryRepository = categoryRepository;
        _categoryFactory = categoryFactory;
        _categoryReadService = categoryReadService;
        _productReadService = productReadService;
    }

    public async Task HandleAsync(CreateCategory command)
    {
        var (id, parentId, name, slug, sortOrder) = command;

        if (await _categoryReadService.ExistsBySlugAsync(slug))
        {
            throw new CategorySlugNotUniqueException(slug);
        }

        if (parentId is { } pid)
        {
            if (!await _categoryReadService.ExistsAsync(pid))
                throw new CategoryNotFoundException(pid);

            if (!await _categoryReadService.IsRootAsync(pid))
                throw new CategoryParentMustBeRootException(pid);

            if (await _productReadService.ExistsByCategoryAsync(pid))
                throw new CategoryWithProductsCannotBecomeParentException(pid);
        }
        
        var category = _categoryFactory.Create(id, parentId, name, slug, sortOrder);
        
        await _categoryRepository.AddAsync(category);
    }
}