using Floristo.Application.Categories.DTOs;
using Floristo.Application.Categories.Queries;
using Floristo.Infrastructure.EF.Categories.Models;
using Floristo.Infrastructure.EF.Contexts;
using Floristo.SharedAbstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace Floristo.Infrastructure.EF.Categories.Queries.Handlers;

internal sealed class GetCategoryHandler : IQueryHandler<GetCategory, CategoryDto?>
{
    private readonly DbSet<CategoryReadModel> _categories;

    public GetCategoryHandler(ReadDbContext dbContext)
    {
        _categories = dbContext.Categories;
    }

    public Task<CategoryDto?> HandleAsync(GetCategory query)
        => _categories
            .Where(c => c.Id == query.Id)
            .AsNoTracking()
            .Select(c => c.AsDto())
            .SingleOrDefaultAsync();
}