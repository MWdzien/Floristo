using Floristo.Application.Categories.Exceptions;
using Floristo.Domain.Categories.Repositories;
using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Categories.Commands.Handlers;

internal sealed class DeleteCategoryDescriptionHandler : ICommandHandler<DeleteCategoryDescription>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryDescriptionHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task HandleAsync(DeleteCategoryDescription command)
    {
        var category = await _categoryRepository.GetAsync(command.Id);

        if (category is null)
        {
            throw new CategoryNotFoundException(command.Id);
        }
        
        category.DeleteDescription();
        
        await _categoryRepository.UpdateAsync(category);
    }
}