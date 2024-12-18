using Keycloak.AuthServices.Login.Models;
using Keycloak.AuthServices.Login.Requests;

namespace Keycloak.AuthServices.Login;

public interface IKeycloakLoginClient
{
    async Task<LoginRepresentation> LoginAsync(
        string clientId,
        string username,
        string password,
        string clientSecret,
        CancellationToken cancellationToken = default
    )
    {
        return await this.SelfLoginAsync(
            new LoginRequestParameters()
            {
                ClientId = clientId,
                ClientSecret = clientSecret,
                Username = username,
                Password = password,                       
            },
            cancellationToken
        );
    }

    Task<LoginRepresentation> SelfLoginAsync(
        LoginRequestParameters? parameters = null,
        CancellationToken cancellationToken = default
    );
}
