namespace Floristo.Application.Auth;

public interface ICurrentUser
{
    public Guid Id { get; }
    public string? Email { get; }
    public string Role { get; }
    public bool IsAuthenticated { get; }
}