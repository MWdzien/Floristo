using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Categories.Commands;

public record ChangeCategorySortOrder(Guid Id, int SortOrder) : ICommand;