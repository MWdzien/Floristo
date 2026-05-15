using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Categories.Commands;

public record DeleteCategoryDescription(Guid Id) : ICommand;