using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Products.Commands;

public record ChangeProductCategory(Guid Id, Guid CategoryId) : ICommand;