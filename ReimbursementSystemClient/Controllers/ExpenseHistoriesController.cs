using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReimbursementSystemAPI.Models;
using ReimbursementSystemAPI.ViewModel;
using ReimbursementSystemClient.Base.Controllers;
using ReimbursementSystemClient.Repository.Data;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace ReimbursementSystemClient.Controllers
{
    public class ExpenseHistoriesController : BaseController<ExpenseHistory, ExpenseHistoryRepository, string>
    {
        public ExpenseHistoriesController(ExpenseHistoryRepository repository) : base(repository)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
