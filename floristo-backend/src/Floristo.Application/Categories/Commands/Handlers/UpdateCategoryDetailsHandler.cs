using Floristo.Application.Categories.Exceptions;
using Floristo.Application.Categories.Services;
using Floristo.Domain.Categories.Repositories;
using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Categories.Commands.Handlers;

internal sealed class UpdateCategoryDetailsHandler : ICommandHandler<UpdateCategoryDetails>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryReadService _categoryReadService;

    public UpdateCategoryDetailsHandler(ICategoryRepository categoryRepository, ICategoryReadService categoryReadService)
    {
        _categoryRepository = categoryRepository;
        _categoryReadService = categoryReadService;
    }

    public async Task HandleAsync(UpdateCategoryDetails command)
    {
        var (id, name, slug, shortDescription, description, imageUrl, seoTitle, seoDescription) = command;
        var category = await _categoryRepository.GetAsync(id);

        if (category is null)
            throw new CategoryNotFoundException(id);

        if (await _categoryReadService.ExistsBySlugAsync(slug, id))
            throw new CategorySlugNotUniqueException(slug);
        
        category.ChangeName(name);
        category.ChangeSlug(slug);
        
        if (string.IsNullOrWhiteSpace(shortDescription))
            category.DeleteShortDescription();
        else category.ChangeShortDescription(shortDescription);
        
        if (string.IsNullOrWhiteSpace(description))
            category.DeleteDescription();
        else category.ChangeDescription(description);
        
        if (string.IsNullOrWhiteSpace(imageUrl))
            category.DeleteImageUrl();
        else category.ChangeImageUrl(imageUrl);

        if (string.IsNullOrWhiteSpace(seoTitle))
            category.DeleteSeoTitle();
        else category.ChangeSeoTitle(seoTitle);
        
        if (string.IsNullOrWhiteSpace(seoDescription))
            category.DeleteSeoDescription();
        else category.ChangeSeoDescription(seoDescription);
        
        await _categoryRepository.UpdateAsync(category);
    }
}