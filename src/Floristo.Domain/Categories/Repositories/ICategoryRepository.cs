using Floristo.Domain.Categories.Entities;
using Floristo.Domain.Categories.ValueObjects;

namespace Floristo.Domain.Categories.Repositories;

public interface ICategoryRepository
{
    Task<Category?> GetAsync(CategoryId id);
    Task AddSync(Category category);
    Task UpdateSync(Category category);
    Task DeleteAsync(Category category);
}