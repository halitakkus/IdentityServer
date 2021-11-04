using IdentityServer.AuthServer.Consts;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer.AuthServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource("resource_api1")
                {
                    Scopes = { Const.FirstApiWrite, Const.FirstApiRead, Const.FirstApiUpdate },
                    ApiSecrets = new []{new Secret("secretapi1".Sha256())}
                },
                new ApiResource("resource_api2")
                 {
                    Scopes = { Const.SecondApiWrite, Const.SecondApiRead, Const.SecondApiUpdate },
                    ApiSecrets = new []{new Secret("secretapi2".Sha256())}
                 },
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>()
            {
                new ApiScope(Const.FirstApiRead,"API 1 için okuma izni."),
                new ApiScope(Const.FirstApiWrite,"API 1 için yazma izni."),
                new ApiScope(Const.FirstApiUpdate,"API 1 için güncelleme izni."),
                new ApiScope(Const.SecondApiRead,"API 2 için okuma izni."),
                new ApiScope(Const.SecondApiWrite,"API 2 için yazma izni."),
                new ApiScope(Const.SecondApiUpdate,"API 2 için güncelleme izni."),
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>()
            {
                new IdentityResources.Email(),
              new IdentityResources.OpenId(),
              new IdentityResources.Profile(),
              new IdentityResource()
              {
                  Name = "CountryAndCity",
                  DisplayName = "Country and city",
                  Description = "Kullanıcının ülke ve şehir bilgisi",
                  UserClaims = new [] { "country", "city" }
              },
              new IdentityResource
              {
                  Name = "Roles",
                  DisplayName = "Roles",
                  Description = "Kullanıcı rolleri",
                  UserClaims = new [] {"role"}
              }
            };
        }

        public static IEnumerable<TestUser> GetUsers()
        {
            return new List<TestUser>()
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "halitakkus",
                    Password = "pass",
                    Claims = new List<Claim>()
                    {
                        new Claim("given_name","Halit"),
                        new Claim("family_name","Akkuş"),
                        new Claim("country","Türkiye"),
                        new Claim("city","Ankara"),
                        new Claim("role", "admin")
                    }
                },
                 new TestUser
                {
                    SubjectId = "2",
                    Username = "Absussametakkus",
                    Password = "pass",
                    Claims = new List<Claim>()
                    {
                        new Claim("given_name","Abdussamet"),
                        new Claim("family_name","Akkuş"),
                        new Claim("country","Türkiye"),
                        new Claim("city","İstanbul"),
                        new Claim("role", "customer")
                    }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client()
                {
                    ClientId = "Client1",
                    ClientName = "Client 1 app uygulaması",
                    ClientSecrets = new[] { new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { Const.FirstApiRead }
                },
                 new Client()
                {
                    ClientId = "Client2",
                    ClientName = "Client 2 app uygulaması",
                    ClientSecrets = new[] { new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes =
                     {
                         Const.FirstApiRead,
                         Const.FirstApiUpdate,
                         Const.SecondApiWrite,
                         Const.SecondApiUpdate
                     }
                },
                 new Client()
                {
                    ClientId = "Client1-Mvc",
                    RequirePkce = false,
                    ClientName = "Client1 MVC app uygulaması",
                    ClientSecrets = new[] { new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Hybrid, // Eğer akış tipi "code" olarak seçilirse. Authorization code grant type karşılık gelmiş olur.   
                    RedirectUris = new List<string> { "https://localhost:5006/signin-oidc"},
                   PostLogoutRedirectUris = new List<string> {"https://localhost:5006/signout-callback-oidc"},
                     AllowedScopes =
                     {
                         IdentityServerConstants.StandardScopes.OpenId,
                         IdentityServerConstants.StandardScopes.Profile,
                         Const.FirstApiRead,
                         IdentityServerConstants.StandardScopes.OfflineAccess,
                         "CountryAndCity",
                         "Roles"
                     },
                    AccessTokenLifetime = 2*60*60,
                    AllowOfflineAccess = true, // artık refresh token yayınlanacaktır.
                    RefreshTokenUsage = TokenUsage.ReUse, // refresh token sürekli kullanılabilir halde.
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60) - DateTime.Now).TotalSeconds, //60 gün sonra sona erecektir. 
                    RequireConsent = true //artık uygulamalar için onay ekranı çıkacak.
                },
                  new Client()
                {
                    ClientId = "Client2-Mvc",
                    RequirePkce = false,
                    ClientName = "Client 2 MVC app uygulaması",
                    ClientSecrets = new[] { new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Hybrid, // Eğer akış tipi "code" olarak seçilirse. Authorization code grant type karşılık gelmiş olur.   
                    RedirectUris = new List<string> { "https://localhost:5011/signin-oidc"},
                   PostLogoutRedirectUris = new List<string> {"https://localhost:5011/signout-callback-oidc"},
                     AllowedScopes =
                     {
                         IdentityServerConstants.StandardScopes.OpenId,
                         IdentityServerConstants.StandardScopes.Profile,
                         Const.FirstApiRead,
                         IdentityServerConstants.StandardScopes.OfflineAccess,
                         "CountryAndCity",
                         "Roles"
                     },
                    AccessTokenLifetime = 2*60*60,
                    AllowOfflineAccess = true, // artık refresh token yayınlanacaktır.
                    RefreshTokenUsage = TokenUsage.ReUse, // refresh token sürekli kullanılabilir halde.
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60) - DateTime.Now).TotalSeconds, //60 gün sonra sona erecektir. 
                    RequireConsent = true //artık uygulamalar için onay ekranı çıkacak.
                }
            };
        }
    }
}
