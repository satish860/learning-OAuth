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
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
            { };

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
                    RequireConsent=true,
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
                    }
                    
                }
            };
}