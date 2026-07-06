namespace Floristo.Infrastructure.Auth;

public class FirebaseAuthOptions
{
    public const string SectionName = "Firebase";
    
    public string ProjectId { get; init; } = null!;
    public string Issuer => $"https://securetoken.google.com/{ProjectId}";
    public string Audience => ProjectId;

    public string JwksUrl => "https://www.googleapis.com/service_accounts/v1/jwk/securetoken@system.gserviceaccount.com";
}