namespace Floristo.Application.Products.Services;

public interface IProductReadService
{
    Task<bool> ExistsBySlugAsync(string slug, Guid? excludeId = null);
    Task<bool> HasProductsInCategoryAsync(Guid categoryId);
}