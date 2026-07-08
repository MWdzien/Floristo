using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Products.Commands;

public record AddProductImage(Guid Id, string Url, int SortOrder) : ICommand;