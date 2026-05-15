using Floristo.Application.Categories.Exceptions;
using Floristo.Domain.Categories.Repositories;
using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Categories.Commands.Handlers;

internal sealed class DeleteCategoryImageUrlHandler : ICommandHandler<DeleteCategoryImageUrl>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryImageUrlHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task HandleAsync(DeleteCategoryImageUrl command)
    {
        var category = await _categoryRepository.GetAsync(command.Id);

        if (category is null)
        {
            throw new CategoryNotFoundException(command.Id);
        }
        
        category.DeleteImageUrl();
        
        await _categoryRepository.UpdateAsync(category);
    }
}