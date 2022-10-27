using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ReimbursementSystemClient.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {
        // GET: /<controller>/
        public IActionResult Dashboard()
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
