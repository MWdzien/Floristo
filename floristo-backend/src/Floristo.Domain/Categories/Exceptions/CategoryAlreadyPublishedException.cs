using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Categories.Exceptions;

public class CategoryAlreadyPublishedException() : FloristoException("category already published")
{
    public override int StatusCode => 409;
}