using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Categories.Commands;

public record CreateCategory(Guid Id, Guid? ParentId, string Name, string Slug, int SortOrder) : ICommand;