# How to use

Program.cs
```cs
builder.Services
    .AddKeycloakAdminHttpClient(builder.Configuration)
    // add this, for add token
    .AddHttpMessageHandler<AddTokenForAdminHttpClientHandler>();

 // add this, for login before send admin api
builder.Services.AddKeycloakLoginHttpClient((options) =>
{
    // defualt user for access admin api
    options.ClientId = "myclient";
    options.Username = "myadmin";
    options.Password = "123456";
    options.ClientSecret = "mysecret";
});
```
