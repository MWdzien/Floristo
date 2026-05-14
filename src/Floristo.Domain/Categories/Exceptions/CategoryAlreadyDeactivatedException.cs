using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Categories.Exceptions;

public class CategoryAlreadyDeactivatedException() : FloristoException("category already deactivated")
{
    public override int StatusCode => 409;
}