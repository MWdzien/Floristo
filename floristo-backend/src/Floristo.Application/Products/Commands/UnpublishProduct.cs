using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Products.Commands;

public record UnpublishProduct(Guid Id) : ICommand;