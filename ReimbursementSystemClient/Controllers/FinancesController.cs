using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReimbursementSystemClient.Controllers
{
    //[Authorize(Roles = "Finance,Director")]
    public class FinancesController : Controller
    {
        public IActionResult FDashboard()
        {
            return View();
        }
    }
}
