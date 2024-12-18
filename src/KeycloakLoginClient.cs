using Keycloak.AuthServices.Login.Models;
using Keycloak.AuthServices.Login.Requests;
using Keycloak.AuthServices.Sdk;
using Microsoft.Extensions.Options;

namespace Keycloak.AuthServices.Login;

public class KeycloakLoginClient(
    HttpClient httpClient,
    IOptions<KeycloakLoginOptions> options
) : IKeycloakLoginClient
{
    private readonly HttpClient httpClient = httpClient;
    private readonly KeycloakLoginOptions loginOptions = options.Value;

    public async Task<LoginRepresentation> SelfLoginAsync(
        LoginRequestParameters? parameters = null,
        CancellationToken cancellationToken = default
    )
    {
        var kvp = new List<KeyValuePair<string, string>>
        {
            new("grant_type", "password"),
            new("scope", "roles")
        };

        if (parameters is null)
        {
            kvp.AddRange(
            [
                new("client_id", loginOptions.ClientId!),
                new("username", loginOptions.Username!),
                new("password", loginOptions.Password!),
                new("client_secret", loginOptions.ClientSecret!),
            ]);
        }
        else
        {
            kvp.AddRange(
            [
                new("client_id", parameters.ClientId!),
                new("username", parameters.Username!),
                new("password", parameters.Password!),
                new("client_secret", parameters.ClientSecret!),
            ]);
        }

        var content = new FormUrlEncodedContent(kvp);

        var response = await httpClient.PostAsync("", content, cancellationToken);

        return await response.GetResponseAsync<LoginRepresentation>(cancellationToken) ?? new();
    }
}