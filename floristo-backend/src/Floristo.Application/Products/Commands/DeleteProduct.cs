using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Products.Commands;

public record DeleteProduct(Guid Id) : ICommand;