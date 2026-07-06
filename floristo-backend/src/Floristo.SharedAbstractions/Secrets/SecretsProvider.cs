namespace Floristo.SharedAbstractions.Secrets;

public interface ISecretsProvider
{
    Task<string> GetSecretAsync(string name, CancellationToken ct = default);
}