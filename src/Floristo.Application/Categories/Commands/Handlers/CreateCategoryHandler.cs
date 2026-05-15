using Floristo.Application.Categories.Exceptions;
using Floristo.Application.Categories.Services;
using Floristo.Domain.Categories.Factories;
using Floristo.Domain.Categories.Repositories;
using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Categories.Commands.Handlers;

internal sealed class CreateCategoryHandler : ICommandHandler<CreateCategory>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryFactory _categoryFactory;
    private readonly ICategoryReadService _categoryReadService;

    public CreateCategoryHandler(ICategoryRepository categoryRepository, ICategoryFactory categoryFactory, ICategoryReadService categoryReadService)
    {
        _categoryRepository = categoryRepository;
        _categoryFactory = categoryFactory;
        _categoryReadService = categoryReadService;
    }

    public async Task HandleAsync(CreateCategory command)
    {
        var (id, name, slug, sortOrder) = command;

        if (await _categoryReadService.ExistsBySlugAsync(slug))
        {
            throw new CategorySlugNotUniqueException(slug);
        }
        
        var category = _categoryFactory.Create(id, name, slug, sortOrder);
        
        await _categoryRepository.AddAsync(category);
    }
}