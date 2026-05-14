using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Categories.Exceptions;

public class CategoryAlreadyActiveException() : FloristoException("category already active")
{
    public override int StatusCode => 409;
}