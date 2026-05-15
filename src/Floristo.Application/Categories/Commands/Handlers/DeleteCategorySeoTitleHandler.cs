using Floristo.Application.Categories.Exceptions;
using Floristo.Domain.Categories.Repositories;
using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Categories.Commands.Handlers;

internal sealed class DeleteCategorySeoTitleHandler : ICommandHandler<DeleteCategorySeoTitle>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategorySeoTitleHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task HandleAsync(DeleteCategorySeoTitle command)
    {
        var category = await _categoryRepository.GetAsync(command.Id);

        if (category is null)
        {
            throw new CategoryNotFoundException(command.Id);
        }
        
        category.DeleteSeoTitle();
        
        await _categoryRepository.UpdateAsync(category);
    }
}