using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Products.Commands;

public record DisallowProductGiftTag(Guid Id) : ICommand;