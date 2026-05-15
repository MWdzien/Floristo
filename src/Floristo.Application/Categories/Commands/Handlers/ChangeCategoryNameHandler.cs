using Floristo.Application.Categories.Exceptions;
using Floristo.Domain.Categories.Repositories;
using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Categories.Commands.Handlers;

internal sealed class ChangeCategoryNameHandler : ICommandHandler<ChangeCategoryName>
{
    private readonly ICategoryRepository _categoryRepository;

    public ChangeCategoryNameHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task HandleAsync(ChangeCategoryName command)
    {
        var category = await _categoryRepository.GetAsync(command.Id);

        if (category is null)
        {
            throw new CategoryNotFoundException(command.Id);
        }
        
        category.ChangeName(command.Name);
        
        await _categoryRepository.UpdateAsync(category);
    }
}