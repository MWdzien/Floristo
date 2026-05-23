namespace Floristo.Application.Categories.Services;

public interface ICategoryReadService
{
    Task<bool> ExistsBySlugAsync(string slug, Guid? excludeId = null);
    Task<bool> ExistsAsync(Guid id);
}