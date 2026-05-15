namespace Floristo.Application.Categories.Services;

public interface ICategoryReadService
{
    Task<bool> ExistsBySlugAsync(string slug);
}