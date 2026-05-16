using Floristo.Application.Categories.Exceptions;
using Floristo.Application.Categories.Services;
using Floristo.Domain.Categories.Repositories;
using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Categories.Commands.Handlers;

internal sealed class ChangeCategorySlugHandler : ICommandHandler<ChangeCategorySlug>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryReadService _categoryReadService;

    public ChangeCategorySlugHandler(ICategoryRepository categoryRepository, ICategoryReadService categoryReadService)
    {
        _categoryRepository = categoryRepository;
        _categoryReadService = categoryReadService;
    }

    public async Task HandleAsync(ChangeCategorySlug command)
    {
        var category = await _categoryRepository.GetAsync(command.Id);

        if (category is null)
        {
            throw new CategoryNotFoundException(command.Id);
        }

        if (await _categoryReadService.ExistsBySlugAsync(command.Slug, command.Id))
        {
            throw new CategorySlugNotUniqueException(command.Slug);
        }
        
        category.ChangeSlug(command.Slug);
        
        await _categoryRepository.UpdateAsync(category);
    }
}