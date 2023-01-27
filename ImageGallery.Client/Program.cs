using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(configure =>
        configure.JsonSerializerOptions.PropertyNamingPolicy = null);

// create an HttpClient used for accessing the API
builder.Services.AddHttpClient("APIClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ImageGalleryAPIRoot"]);
    client.DefaultRequestHeaders.Clear();
    client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
});

builder.Services.AddAuthentication(c =>
{
    c.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    c.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
}).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
{
    o.AccessDeniedPath = "/authorize/accessdenied";
})
 .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, c =>
    {
        c.SignInScheme=CookieAuthenticationDefaults.AuthenticationScheme;
        c.SaveTokens = true;
        c.ResponseType = "code";
        c.Authority = "https://localhost:5001";
        c.ClientId = "imagegallery";
        c.ClientSecret = "secret";
        c.GetClaimsFromUserInfoEndpoint = true;
        c.Scope.Add("roles");
        c.Scope.Add("api.full");
        c.ClaimActions.MapJsonKey("role", "role");
        c.TokenValidationParameters = new()
        {
            NameClaimType = "given_name",
            RoleClaimType = "role"
        };

    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Gallery}/{action=Index}/{id?}");

app.Run();
