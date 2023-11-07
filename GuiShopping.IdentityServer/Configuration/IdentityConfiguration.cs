using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace GuiShopping.IdentityServer.Configuration
{
    public static class IdentityConfiguration
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";

        public static IEnumerable<IdentityResource> identityResources =>
            new List<IdentityResource>
            {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
            };
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("gui_shopping", "GuiShopping server"),
                new ApiScope(name:"read", "Read Data"),
                new ApiScope(name:"write", "Write Data"),
                new ApiScope(name:"delete", "Delete Data")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = {new Secret("My_super_secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes= { "read" , "write" , "profile" }
                },
                new Client
                {
                    ClientId = "gui_shopping",
                    ClientSecrets = {new Secret("My_super_secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris={"http://localhost:5182/signin-oidc"},
                    PostLogoutRedirectUris={ "http://localhost:5182/signout-callback-oidc" },
                    AllowedScopes= new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "gui_shopping"
                    }
                }
            };
    }
}
