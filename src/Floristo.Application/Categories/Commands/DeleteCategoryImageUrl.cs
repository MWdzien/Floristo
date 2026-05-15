using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Categories.Commands;

public record DeleteCategoryImageUrl(Guid Id) : ICommand;