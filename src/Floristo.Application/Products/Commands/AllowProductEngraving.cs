using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Products.Commands;

public record AllowProductEngraving(Guid Id) : ICommand;