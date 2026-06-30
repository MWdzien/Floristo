namespace Floristo.Infrastructure.Auth;

public class SupabaseAuthOptions
{
    public const string SectionName = "Supabase";
    
    public string ProjectUrl { get; init; } = null!;
    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
    public string PublishableKey { get; init; } = null!;

    public string JwksUrl => $"{ProjectUrl}/auth/v1/.well-known/jwks.json";
}