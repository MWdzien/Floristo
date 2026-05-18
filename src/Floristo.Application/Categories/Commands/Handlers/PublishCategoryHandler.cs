using Floristo.Application.Categories.Exceptions;
using Floristo.Domain.Categories.Repositories;
using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Categories.Commands.Handlers;

internal sealed class PublishCategoryHandler : ICommandHandler<PublishCategory>
{
    private readonly ICategoryRepository _categoryRepository;

    public PublishCategoryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task HandleAsync(PublishCategory command)
    {
        var category = await _categoryRepository.GetAsync(command.Id);

        if (category is null)
        {
            throw new CategoryNotFoundException(command.Id);
        }
        
        category.Publish();
        
        await _categoryRepository.UpdateAsync(category);
    }
}