using Floristo.Application.Categories.Exceptions;
using Floristo.Domain.Categories.Repositories;
using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Categories.Commands.Handlers;

internal sealed class DeactivateCategoryHandler : ICommandHandler<DeactivateCategory>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeactivateCategoryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task HandleAsync(DeactivateCategory command)
    {
        var category = await _categoryRepository.GetAsync(command.Id);

        if (category is null)
        {
            throw new CategoryNotFoundException(command.Id);
        }
        
        category.Deactivate();
        
        await _categoryRepository.UpdateAsync(category);
    }
}