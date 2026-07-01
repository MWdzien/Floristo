using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Products.Commands;

public record DisallowProductEngraving(Guid Id) : ICommand;