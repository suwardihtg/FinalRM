using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReimbursementSystemClient.Controllers
{
    //[Authorize]
    public class ReimbusmentsController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }

        public IActionResult Input()
        {
            return View();
        }

        public IActionResult Expense()
        {
            return View();
        }

        public IActionResult Reimbusment()
        {
            return View();
        }
    }
}
