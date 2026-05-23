using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Products.Commands;

public record CreateProduct(
    Guid Id,
    string Name, 
    string Slug, 
    decimal Price, 
    Guid CategoryId,
    int SortOrder
) : ICommand;