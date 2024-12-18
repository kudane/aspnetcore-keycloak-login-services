using System.Net.Http.Headers;

namespace Keycloak.AuthServices.Login.Admin;

public class AddTokenForAdminHttpClientHandler(IKeycloakLoginClient loginClient) : DelegatingHandler
{
    private readonly IKeycloakLoginClient loginClient = loginClient;

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var response = await loginClient.SelfLoginAsync(cancellationToken: cancellationToken);

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", response.AccessToken);

        return await base.SendAsync(request, cancellationToken);
    }
}