namespace Floristo.SharedAbstractions.Exceptions;

public class FloristoException : Exception
{
    public virtual int StatusCode => 400;
    
    protected FloristoException(string message) : base(message)
    {
    }
}