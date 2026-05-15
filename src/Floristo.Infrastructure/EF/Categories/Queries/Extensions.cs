using Floristo.Application.Categories.DTOs;
using Floristo.Infrastructure.EF.Categories.Models;

namespace Floristo.Infrastructure.EF.Categories.Queries;

internal static class Extensions
{
    internal static CategoryDto AsDto(this CategoryReadModel category)                                                                                                                                             
        => new(
            category.Id,
            category.Name,
            category.Description,
            category.Slug,
            category.SortOrder,
            category.ImageUrl,
            category.SeoTitle,
            category.SeoDescription,
            category.CreatedAt,
            category.DeactivatedAt
            );
}