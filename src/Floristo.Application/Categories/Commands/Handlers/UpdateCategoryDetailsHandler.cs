using Floristo.Application.Categories.Exceptions;
using Floristo.Domain.Categories.Repositories;
using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Categories.Commands.Handlers;

internal sealed class UpdateCategoryDetailsHandler : ICommandHandler<UpdateCategoryDetails>
{
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryDetailsHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task HandleAsync(UpdateCategoryDetails command)
    {
        var (id, description, imageUrl, seoTitle, seoDescription) = command;
        var category = await _categoryRepository.GetAsync(id);

        if (category is null)
        {
            throw new CategoryNotFoundException(id);
        }
        
        if (description is not null)
        {
            category.ChangeDescription(description);
        }        
        if (imageUrl is not null)
        {
            category.ChangeImageUrl(imageUrl);
        }        
        if (seoTitle is not null)
        {
            category.ChangeSeoTitle(seoTitle);
        }        
        if (seoDescription is not null)
        {
            category.ChangeSeoDescription(seoDescription);
        }        
        
        
        await _categoryRepository.UpdateAsync(category);
    }
}