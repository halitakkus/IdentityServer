using IdentityServer.API2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.API2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PicturesController : Controller
    {
        [Authorize]
        [HttpGet]
        public IActionResult GetPictures()
        {
            var pictures = new List<Picture>()
            {
                new Picture
                {
                     Name = "pc1",
                     Url = "http://127.0.0.1"
                },
                 new Picture
                {
                     Name = "pc2",
                     Url = "http://127.0.0.1/test"
                }
            };

            return View(pictures);
        }
    }
}
