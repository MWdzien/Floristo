using Floristo.SharedAbstractions.Secrets;
using Google.Cloud.SecretManager.V1;

namespace Floristo.Infrastructure.Secrets;

internal sealed class GoogleSecretsProvider : ISecretsProvider
{
    private readonly SecretManagerServiceClient _client;
    private readonly GoogleSecretsOptions _options;
    
    public GoogleSecretsProvider(GoogleSecretsOptions options)
    {
        _options = options;
        _client = SecretManagerServiceClient.Create();
    }

    public async Task<string> GetSecretAsync(string name, CancellationToken ct = default)
    {
        var secretVersionName = new SecretVersionName(_options.ProjectId, name, "latest");

        var response = await _client.AccessSecretVersionAsync(secretVersionName, ct);

        return response.Payload.Data.ToStringUtf8();
    }
}