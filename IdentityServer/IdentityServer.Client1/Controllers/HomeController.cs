using IdentityModel.Client;
using IdentityServer.Client1.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IdentityServer.Client1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            List<Product> products = new List<Product>();

            HttpClient httpClient = new HttpClient();

            var accessToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            httpClient.SetBearerToken(accessToken);

            //https://localhost:5006

            httpClient.SetBearerToken(accessToken);

            var response = await httpClient.GetAsync("https://localhost:6001/api/product/getproducts");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                products = JsonConvert.DeserializeObject<List<Product>>(content);
            }
            else
            {
                //LOG
            }

            return View(products);
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            ViewBag.url = ReturnUrl;

            return View();
        }
    }
}
