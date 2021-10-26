using IdentityServer.AuthServer.Consts;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
                    Scopes = { Const.FirstApiWrite, Const.FirstApiRead, Const.FirstApiUpdate } ,
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
              new IdentityResources.OpenId(),
              new IdentityResources.Profile()
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
                        new Claim("family_name","Akkuş")
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
                        new Claim("family_name","Akkuş")
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
                    AllowedScopes = {Const.FirstApiRead, Const.FirstApiUpdate, Const.SecondApiWrite, Const.SecondApiUpdate }
                }
            };
        }
    }
}
