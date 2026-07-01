using Floristo.Application.Categories.Exceptions;
using Floristo.Domain.Categories.Repositories;
using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Categories.Commands.Handlers;

internal sealed class UnpublishCategoryHandler : ICommandHandler<UnpublishCategory>
{
    private readonly ICategoryRepository _categoryRepository;

    public UnpublishCategoryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task HandleAsync(UnpublishCategory command)
    {
        var category = await _categoryRepository.GetAsync(command.Id);

        if (category is null)
        {
            throw new CategoryNotFoundException(command.Id);
        }
        
        category.Unpublish();
        
        await _categoryRepository.UpdateAsync(category);
    }
}