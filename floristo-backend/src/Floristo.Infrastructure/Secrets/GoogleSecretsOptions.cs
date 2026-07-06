namespace Floristo.Infrastructure.Secrets;

public class GoogleSecretsOptions
{
    public const string SectionName = "GoogleSecrets";
    public string ProjectId { get; init; } = null!;
}