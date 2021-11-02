﻿using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IdentityServer.Client1.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task LogOut()
        {
            await HttpContext.SignOutAsync("Cookies"); //IdentityServer4 üzerinden çıkış yapar.
            await HttpContext.SignOutAsync("oidc"); //Kendi uygulamamızdan çıkış yapar.
        }

        public async Task<IActionResult> GetRefreshToken()
        {
            var httpClient = new HttpClient();

            var disco = await httpClient.GetDiscoveryDocumentAsync("https://localhost:5001");

            if (disco.IsError)
            {
                //loglama
            }

            var refhreshToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

            var refreshTokenRequest = new RefreshTokenRequest
            {
                ClientId = _configuration["Client1Mvc:ClientId"],
                ClientSecret = _configuration["Client1Mvc:ClientSecret"],
                RefreshToken = refhreshToken,
                Address = disco.TokenEndpoint
            };

            var token = await httpClient.RequestRefreshTokenAsync(refreshTokenRequest);

            if (token.IsError)
            {
                //yönlendirme yap
            }
            var tokens = new List<AuthenticationToken>()
            {
                new AuthenticationToken
                { 
                    Name=OpenIdConnectParameterNames.IdToken,Value= token.IdentityToken
                },
                      new AuthenticationToken
                      { 
                          Name=OpenIdConnectParameterNames.AccessToken,Value= token.AccessToken
                      },
                            new AuthenticationToken
                            { 
                                Name=OpenIdConnectParameterNames.RefreshToken,Value= token.RefreshToken
                            },
                                  new AuthenticationToken
                                  { 
                                      Name=OpenIdConnectParameterNames.ExpiresIn,Value= DateTime.UtcNow.AddSeconds(token.ExpiresIn).ToString("o", CultureInfo.InvariantCulture)
                                  }
            };

            var authenticationResult = await HttpContext.AuthenticateAsync();

            var properties = authenticationResult.Properties;

            properties.StoreTokens(tokens);

            await HttpContext.SignInAsync("Cookies", authenticationResult.Principal, properties);

            return RedirectToAction("Index");
        }
    }
}
