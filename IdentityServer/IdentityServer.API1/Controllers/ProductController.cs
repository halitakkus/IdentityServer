using IdentityServer.API1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.API1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : Controller
    {
        [Authorize]
        [HttpGet]
        public IActionResult GetProducts()
        {
            var productList = new List<Product>()
           {
               new Product
               {
                   Id = 1,
                   Name = "Kalep",
                   Price = 100,
                   Stock = 500
               },
               new Product
               {
                   Id = 2,
                   Name = "açacak",
                   Price = 200,
                   Stock = 100
               },
               new Product
               {
                   Id = 3,
                   Name = "silgi",
                   Price = 50,
                   Stock = 200
               }
           };

            return Ok(productList);
        }
    }
}
