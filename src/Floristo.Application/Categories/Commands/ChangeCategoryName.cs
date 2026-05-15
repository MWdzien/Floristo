using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Categories.Commands;

public record ChangeCategoryName(Guid Id, string Name) : ICommand;