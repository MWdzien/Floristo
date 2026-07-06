namespace Floristo.Infrastructure.EF.Options;

public class PostgresOptions
{
    public const string SectionName = "Postgres";
    
    public string Host { get; init; } = null!;
    public int Port { get; init; } = 5432;
    public string Database { get; init; } = null!;                                                                                                                                                                 
    public string Username { get; init; } = null!;
    public string PasswordSecretName { get; init; } = null!;                                                                                                                                                       
    public string SslMode { get; init; } = "Disable";
}