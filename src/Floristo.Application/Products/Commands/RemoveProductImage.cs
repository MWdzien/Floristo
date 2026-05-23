using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Products.Commands;

public record RemoveProductImage(Guid Id, Guid ImageId) : ICommand;