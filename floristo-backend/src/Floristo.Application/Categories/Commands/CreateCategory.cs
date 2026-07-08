using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Categories.Commands;

public record CreateCategory(Guid? ParentId, string Name, string Slug) : ICommand;