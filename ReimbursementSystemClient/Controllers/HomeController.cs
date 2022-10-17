using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReimbursementSystemClient.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ReimbursementSystemClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index2()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Upload()
        {
            return View();
        }

        public IActionResult Login()
        {
            HttpContext.Session.SetString("Test", "Ben Rules!");
            return View();
        }

        [HttpGet("Notfound/")]
        public IActionResult Page_404()
        {
            return View();
        }

        [HttpGet("Forbidden/")]
        public IActionResult Page_403()
        {
            return View("Page_403");
        }

        [HttpGet("Unauthorized/")]
        public IActionResult Page_401()
        {
            return View("Page_401");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
