using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ReimbursementSystemAPI.Base;
using ReimbursementSystemAPI.Models;
using ReimbursementSystemAPI.Repository.Data;
using ReimbursementSystemAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : BaseController<Expense, ExpenseRepository, int>
    {
        private ExpenseRepository expenseRepository;
        public IConfiguration _configuration;
        private readonly MyContext context;

        public ExpensesController(ExpenseRepository repository, IConfiguration configuration, MyContext context) : base(repository)
        {
            this.expenseRepository = repository;
            this._configuration = configuration;
            this.context = context;
        }

        //Add New Request
        [HttpPost("ExpenseInsert")]
        public ActionResult ExpenseForm(ExpenseVM expenseVM)
        {
            var result = expenseRepository.ExpenseForm(expenseVM);
            switch (result)
            {
                case 1:
                    return Ok(); 
                default:
                    return BadRequest();
            }
        }

        //Update Request and History
        [HttpPut("ExpenseUpdate/{code}")]
        public ActionResult ExpenseFormUpdate(ExpenseVM expenseVM, int code)
        {
            var result = expenseRepository.ExpenseFormUpdate(expenseVM, code);
            if (result == 1)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        //Get Expense by Id
        [HttpGet("ExpenseData/{employeeid}")]
        public ActionResult GetExpense(string employeeid)
        {
            var result = expenseRepository.GetExpense(employeeid);
            
            if (result.Count() != 0)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        //Get Expense Id by Email
        [HttpGet("GetID/{email}")]
        public ActionResult ExpesnseID(string email)
        {
            var result = expenseRepository.ExpesnseID(email);

            if (result != null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }



        //----------------- Finances -------------------

        [HttpGet("ExpenseDataFinances")]
        public ActionResult GetExpenseFinances()
        {
            var result = expenseRepository.GetExpenseFinance();

            if (result.Count() != 0)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("ExpenseDataFinancesReject")]
        public ActionResult GetExpenseFinancesReject()
        {
            var result = expenseRepository.GetExpenseFinanceReject();

            if (result.Count() != 0)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("ExpenseDataFinancesPayment")]
        public ActionResult GetExpenseFinancesPayment()
        {
            var result = expenseRepository.GetExpenseFinancePayment();

            if (result.Count() != 0)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpPut("Approval/{code}")]
        public ActionResult Approval(ExpenseVM expenseVM, int code)
        {

            var result = expenseRepository.ExpenseFormUpdate(expenseVM, code);
            if(result == 1)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
            
        }


        //----------------- Manager -------------------

        [HttpGet("ExpenseDataManager")]
        public ActionResult GetExpenseManager()
        {
            var result = expenseRepository.GetExpenseManager();

            if (result.Count() != 0)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("GetExpenseManagerReject")]
        public ActionResult GetExpenseManagerReject()
        {
            var result = expenseRepository.GetExpenseManagerReject();

            if (result.Count() != 0)
            {
                return Ok(result);
            }
            return NotFound(result);
        }



        //----------------- Manager & Finances -------------------

        [HttpGet("GetExpensePosted")]
        public ActionResult GetExpensePosted()
        {
            var result = expenseRepository.GetExpensePosted();

            if (result.Count() != 0)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}
