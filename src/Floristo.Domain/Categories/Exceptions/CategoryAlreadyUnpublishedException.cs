using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Categories.Exceptions;

public class CategoryAlreadyUnpublishedException() : FloristoException("category already unpublished")
{
    public override int StatusCode => 409;
}