using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Products.Commands;

public record CreateProduct(
    string Name, 
    string Slug, 
    decimal Price, 
    Guid CategoryId,
    int SortOrder
) : ICommand;