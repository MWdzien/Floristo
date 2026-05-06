namespace Floristo.SharedAbstractions.Exceptions;

public class FloristoException : Exception
{
    protected FloristoException(string message) : base(message)
    {
    }
}