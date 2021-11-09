// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer_IdentityAPI.AuthServer.Consts;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace IdentityServer_IdentityAPI.AuthServer
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
                new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
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
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
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

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client()
                {
                    ClientId = "Client1",
                    ClientName = "Client 1 app uygulaması",
                    ClientSecrets = new[] { new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    //hem istemci bazlı yetkilendirme hem kullanıcı bazlı yetkilendirme.
                    AllowedScopes =
                    { 
                        IdentityServerConstants.StandardScopes.Email, IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile, "api1.read","CountryAndCity","Roles", IdentityServerConstants.LocalApi.ScopeName,
                        IdentityServerConstants.StandardScopes.OfflineAccess
                    },
                    AccessTokenLifetime=2*60*60,
                   AllowOfflineAccess=true,
                   RefreshTokenUsage=TokenUsage.ReUse,
                   RefreshTokenExpiration=TokenExpiration.Absolute,
                   AbsoluteRefreshTokenLifetime=(int) (DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
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
                         IdentityServerConstants.StandardScopes.Email,
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
                    ClientSecrets = new[] { new Secret("secret".Sha256())}, // spa ve mobil app ler üzerinde kullanılmaz, güvenlik açığına sebebiyet verir. Spa lar tarayıcı üzerinde çalışır.
                    AllowedGrantTypes = GrantTypes.Hybrid, // Eğer akış tipi "code" olarak seçilirse. Authorization code grant type karşılık gelmiş olur.   
                    RedirectUris = new List<string> { "https://localhost:5011/signin-oidc"},
                   PostLogoutRedirectUris = new List<string> {"https://localhost:5011/signout-callback-oidc"},
                     AllowedScopes =
                     {
                         IdentityServerConstants.StandardScopes.Email,
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
                  new Client
                  {
                      ClientId = "js-client",
                      RequireClientSecret = false, // güvenlik açığına sebebiyet vermesin diye "false" seçildi.
                      ClientName = "Js Client (Angular)",
                      AllowedScopes =
                     {
                         IdentityServerConstants.StandardScopes.Email,
                         IdentityServerConstants.StandardScopes.OpenId,
                         IdentityServerConstants.StandardScopes.Profile,
                         Const.FirstApiRead,
                         IdentityServerConstants.StandardScopes.OfflineAccess,
                         "CountryAndCity",
                         "Roles"
                     },
                      RedirectUris = {"http://localhost:4200/callback"},
                      AllowedCorsOrigins = {"http://localhost:4200"},
                      PostLogoutRedirectUris = {"http://localhost:4200"},
                      AllowedGrantTypes = GrantTypes.Code // Authorization code grant akış tipini işaret eder.
                  }
            };
        }
    }
}