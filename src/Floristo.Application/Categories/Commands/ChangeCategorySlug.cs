using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Categories.Commands;

public record ChangeCategorySlug(Guid Id, string Slug) : ICommand;