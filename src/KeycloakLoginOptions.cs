namespace Keycloak.AuthServices.Login;

public sealed class KeycloakLoginOptions
{
    public string ClientId { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string ClientSecret { get; set; } = default!;
}
