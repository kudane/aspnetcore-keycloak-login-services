using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Login;
using Keycloak.AuthServices.Login.Admin;
using Keycloak.AuthServices.Sdk;
using Keycloak.AuthServices.Sdk.Admin;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add keycloak authen service
builder.Services.AddKeycloakWebApiAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

// add keycloak admin api service
builder.Services
    .AddKeycloakAdminHttpClient(builder.Configuration)
    // add token from "AddKeycloakLoginHttpClient()" to headers when call to admin api
    .AddHttpMessageHandler<AddTokenForAdminHttpClientHandler>();

// add keycloak login service
builder.Services.AddKeycloakLoginHttpClient((options) =>
{
    // defualt user for access admin api
    options.ClientId = "myclient";
    options.Username = "myadmin";
    options.Password = "123456";
    options.ClientSecret = "mysecret";
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/get/users/count", async (IKeycloakClient adminApi) =>
{
    var count = await adminApi.GetUserCountAsync("myrealm");
    return count;
});

app.Run();