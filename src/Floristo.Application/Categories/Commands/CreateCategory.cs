using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Categories.Commands;

public record CreateCategory(Guid Id, string Name, string Slug, int SortOrder) : ICommand;