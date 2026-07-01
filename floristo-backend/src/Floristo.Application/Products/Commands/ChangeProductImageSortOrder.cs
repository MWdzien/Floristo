using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Products.Commands;

public record ChangeProductImageSortOrder(Guid Id, Guid ImageId, int SortOrder) : ICommand;