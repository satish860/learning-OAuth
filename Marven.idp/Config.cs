using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Marven.idp;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource("roles","Your Role(s)",new[] {"role"}),
            new IdentityResource("country","Your Country(s)",new[] {"country"}),
        };

    public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
    {
        new ApiResource("imagegalleryapi","Image Gallery API",new[]{"role"})
        {
            Scopes = { "imagegalleryapi.full" }
        }
    };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
            {
                new ApiScope("imagegalleryapi.full","Image Gallery Full access")
            };

    public static IEnumerable<Client> Clients =>
        new Client[] 
            { 
                new Client
                {
                    ClientId = "imagegallery",
                    ClientName= "Image Gallery",
                    RedirectUris= new[]
                    {
                        "https://localhost:7184/signin-oidc"
                    },
                    PostLogoutRedirectUris =
                    {
                        "https://localhost:7184/signout-callback-oidc"
                    },
                    RequireConsent=true,
                    AllowOfflineAccess=true,
                    AllowedGrantTypes = new []
                    {
                        GrantType.AuthorizationCode
                    },
                    ClientSecrets= new[]
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = new []
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "roles",
                        "imagegalleryapi.full",
                        "country"

                    }
                    
                }
            };
}