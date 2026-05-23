using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Products.Commands;

public record AllowProductGiftTag(Guid Id) : ICommand;