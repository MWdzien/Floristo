using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Application.Categories.Exceptions;

public class CategorySlugNotUniqueException(string slug) : FloristoException($"category with slug {slug} already exists");