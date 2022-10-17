using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReimbursementSystemAPI.Models;
using ReimbursementSystemAPI.ViewModel;
using ReimbursementSystemClient.Base.Controllers;
using ReimbursementSystemClient.Repository.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Threading.Tasks;

namespace ReimbursementSystemClient.Controllers
{
    public class ExpensesController : BaseController<Expense, ExpenseRepository, string>
    {
        private readonly ExpenseRepository expensesRepository;

        public ExpensesController(ExpenseRepository repository) : base(repository)
        {
            this.expensesRepository = repository;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }

        public IActionResult Expense()
        {
            return View();
        }

        [Authorize]
        public IActionResult Reimbusment()
        {
            return View();
        }


        [HttpGet]
        public async Task<JsonResult> GetExpense()
        {
            var sessionId = HttpContext.Session.GetString("EmployeeId");
            var result = await expensesRepository.GetExpense(sessionId);
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> NewExpense(ExpenseVM entity)
        {
            var sessionId = HttpContext.Session.GetString("EmployeeId");
            var result = expensesRepository.NewExpense(entity, sessionId);

            var sessionEmail = HttpContext.Session.GetString("Email");
            var result2 = await expensesRepository.GetID(sessionEmail);
            HttpContext.Session.SetString("ExpenseID", result2.ExpenseID.ToString());

            return Json(result);
        }

        [Route("~/Expenses/Submit/{code}")]
        public JsonResult Submit(ExpenseVM entity, int code)
        {
            var sessionId = HttpContext.Session.GetString("EmployeeId");
            var result = expensesRepository.Submit(entity, sessionId, code);
            return Json(result);
        }

        [Route("~/Expenses/EditExpense/{expenseid}")]
        public JsonResult EditExpense(int expenseid)
        {
            HttpContext.Session.SetString("ExpenseID", expenseid.ToString());
            return Json(expenseid);
        }

        [Route("~/Expenses/ExpenseCall/")]
        public JsonResult EditExpenseCall()
        {
            var expenseSession = HttpContext.Session.GetString("ExpenseID");
            HttpContext.Session.SetString("ExpenseID", expenseSession);
            return Json(expenseSession);
        }

        [Route("~/Expenses/GetHistory/{expenseid}")]
        public async Task<JsonResult> GetHistory(int expenseid)
        {
            var result = await expensesRepository.GetHistory(expenseid);
            return Json(result);
        }

        //<!----------------- Finances ------------------->

        [HttpGet]
        public async Task<JsonResult> GetExpenseFinance()
        {
            var result = await expensesRepository.GetExpenseFinance();
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetExpenseFinanceReject()
        {
            var result = await expensesRepository.GetExpenseFinanceReject();
            return Json(result);
        }


        //<!----------------- Manager ------------------->

        [HttpGet]
        public async Task<JsonResult> GetExpenseManager()
        {
            var result = await expensesRepository.GetExpenseManager();
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetExpenseSManager()
        {
            var result = await expensesRepository.GetExpenseSManager();
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetExpenseDirector()
        {
            var result = await expensesRepository.GetExpenseDirector();
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetExpenseManagerReject()
        {
            var result = await expensesRepository.GetExpenseManagerReject();
            return Json(result);
        }


        //<!----------------- Manager & Finances -------------------> 

        [HttpGet]
        public async Task<JsonResult> GetExpensePosted()
        {
            var result = await expensesRepository.GetExpensePosted();
            return Json(result);
        }

        [Route("~/Expenses/Approval/{code}")]
        public JsonResult Put(ExpenseVM expenseVM, int Code)
        {
            var sessionId = HttpContext.Session.GetString("EmployeeId");
            var result = expensesRepository.Approval(expenseVM, Code);
            return Json(result);
        }

    }
}
