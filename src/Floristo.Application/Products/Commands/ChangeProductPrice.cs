using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Products.Commands;

public record ChangeProductPrice(Guid Id, decimal Price) : ICommand;