using Keycloak.AuthServices.Login.Admin;
using Keycloak.AuthServices.Sdk;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Keycloak.AuthServices.Login;

public static class ServiceCollectionExtensions
{
    public static IHttpClientBuilder AddKeycloakLoginHttpClient(
        this IServiceCollection services,
        Action<KeycloakLoginOptions> configureKeycloakLoginOptions
    )
    {
        services.Configure(configureKeycloakLoginOptions);
        services.AddTransient<AddTokenForAdminHttpClientHandler>();
        services.AddTransient<IKeycloakLoginClient, KeycloakLoginClient>();

        return services
            .AddHttpClient(
                "keycloak_login_api",
                (sp, http) =>
                {
                    var keycloakOptions = sp.GetRequiredService<IOptions<KeycloakAdminClientOptions>>();

                    if (keycloakOptions is null)
                    {
                        throw new ArgumentException(
                            $"'{nameof(keycloakOptions)}' cannot be null.",
                            nameof(keycloakOptions)
                        );
                    }

                    http.BaseAddress = new Uri(keycloakOptions.Value.KeycloakTokenEndpoint);
                }
            )
            .AddTypedClient<IKeycloakLoginClient, KeycloakLoginClient>();
    }
}
