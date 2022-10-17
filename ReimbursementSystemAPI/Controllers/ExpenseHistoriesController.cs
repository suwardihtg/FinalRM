using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ReimbursementSystemAPI.Base;
using ReimbursementSystemAPI.Models;
using ReimbursementSystemAPI.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseHistoriesController : BaseController<ExpenseHistory, ExpenseHistoryRepository, string>
    {
        private ExpenseHistoryRepository expenseHistoryRepository;
        public IConfiguration _configuration;
        private readonly MyContext context;
        public ExpenseHistoriesController(ExpenseHistoryRepository repository, IConfiguration configuration, MyContext context) : base(repository)
        {
            this.expenseHistoryRepository = repository;
            this._configuration = configuration;
            this.context = context;
        }

        [HttpGet("History/{expenseid}")]
        public ActionResult GetExpenseFinances(int expenseid)
        {
            var result = expenseHistoryRepository.GetHistory(expenseid);

            if (result.Count() != 0)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}
