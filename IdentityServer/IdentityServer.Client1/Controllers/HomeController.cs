using IdentityModel.Client;
using IdentityServer.Client1.Models;
using IdentityServer.Client1.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace IdentityServer.Client1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly IApiResourceHttpClient _apiResourceHttpClient; 

        public HomeController(IConfiguration configuration, IApiResourceHttpClient apiResourceHttpClient)
        {
            _configuration = configuration;
            _apiResourceHttpClient = apiResourceHttpClient;
        }

        public async Task<IActionResult> Index()
        {
            var products = new List<Product>();

            var httpClient = await _apiResourceHttpClient.GetHttpClient();

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
