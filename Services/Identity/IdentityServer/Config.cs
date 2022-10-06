using IdentityServer4.Models;
using static IdentityServer4.IdentityServerConstants;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            //need to be included in oidc request
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email()
        };
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope(
                name: "ApiOneScope",
                displayName:"You can manage the ApiOne system.",
                userClaims: new List<string>{ }
                )
        };

    //need for ApiScopes gouping and for support "aud" parameter of JWT token
    public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>
        {
            new ApiResource("api1", "api1 display name")
            {
                //for the "aud" parameter all scopes allowed for the client should be
                //contained in corresponding ApiResource
                Scopes = { 
                    "ApiOneScope", 
                    StandardScopes.OpenId,
                    StandardScopes.Profile,
                    StandardScopes.Email
                }
            }
        };

    public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "client_id",
                    ClientSecrets =
                    {
                        new Secret("client_secret".Sha256())
                    },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    // scopes that client has access to
                    AllowedScopes = { "ApiOneScope" }
                },
                new Client
                {
                    ClientId = "client_id_mvc",
                    ClientSecrets =
                    {
                        new Secret("client_secret_mvc".Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes = { 
                        "ApiOneScope", 
                        StandardScopes.OpenId,
                        StandardScopes.Profile,
                        StandardScopes.Email
                    },
                    RedirectUris = { "https://localhost:7001/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:7001/Home/Home" },

                    //put all the claims to the id token
                    //AlwaysIncludeUserClaimsInIdToken = true,
                }
            };
}