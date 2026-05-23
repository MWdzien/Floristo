using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Products.Commands;

public record ChangeProductSortOrder(Guid Id, int SortOrder) : ICommand;