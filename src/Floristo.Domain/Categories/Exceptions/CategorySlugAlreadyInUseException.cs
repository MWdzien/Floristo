using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Categories.Exceptions;

public class CategorySlugAlreadyInUseException() : FloristoException("category slug already in use")
{
    public override int StatusCode => 422;
}