using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReimbursementSystemClient.Controllers
{
    //[Authorize(Roles = "Manager,SuperManager,Director")]
    public class ManagerController : Controller
    {
        // GET: /<controller>/
        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult DDashboard()
        {
            return View();
        }

        public IActionResult SMDashboard()
        {
            return View();
        }

        [Authorize]
        public IActionResult Manager()
        {
            return View();
        }
    }
}
