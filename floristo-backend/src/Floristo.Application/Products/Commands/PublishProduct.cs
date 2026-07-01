using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Products.Commands;

public record PublishProduct(Guid Id) : ICommand;