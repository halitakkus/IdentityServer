using IdentityServer.AuthServer.Consts;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    Scopes = { Const.FirstApiWrite, Const.FirstApiRead, Const.FirstApiUpdate } 
                },
                new ApiResource("resource_api2")
                 {
                    Scopes = { Const.SecondApiWrite, Const.SecondApiRead, Const.SecondApiUpdate }
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
                    AllowedScopes = {Const.FirstApiRead, Const.SecondApiWrite, Const.SecondApiUpdate }
                },
                 new Client()
                {
                    ClientId = "Client2",
                    ClientName = "Client 2 app uygulaması",
                    ClientSecrets = new[] { new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {Const.FirstApiRead, Const.SecondApiWrite, Const.SecondApiUpdate }
                }
            };
        }

    }
}
