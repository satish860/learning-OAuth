﻿using Duende.IdentityServer;
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
        };

    public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
    {
        new ApiResource("api","Image Gallery API")
        {
            Scopes = {"api.full"}
        }
    };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
            {
                new ApiScope("api.full","Image Gallery Full access")
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
                        "api.full"
                    }
                    
                }
            };
}