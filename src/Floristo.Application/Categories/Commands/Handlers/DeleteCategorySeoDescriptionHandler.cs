using Floristo.Application.Categories.Exceptions;
using Floristo.Domain.Categories.Repositories;
using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Categories.Commands.Handlers;

internal sealed class DeleteCategorySeoDescriptionHandler : ICommandHandler<DeleteCategorySeoDescription>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategorySeoDescriptionHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task HandleAsync(DeleteCategorySeoDescription command)
    {
        var category = await _categoryRepository.GetAsync(command.Id);

        if (category is null)
        {
            throw new CategoryNotFoundException(command.Id);
        }
        
        category.DeleteSeoDescription();
        
        await _categoryRepository.UpdateAsync(category);
    }
}