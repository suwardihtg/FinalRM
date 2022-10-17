using Microsoft.Extensions.Configuration;
using ReimbursementSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Repository.Data
{
    public class ExpenseHistoryRepository : GeneralRepository<MyContext, ExpenseHistory, string>
    {
        private readonly MyContext context;
        public IConfiguration _configuration;
        public ExpenseHistoryRepository(MyContext myContext, IConfiguration configuration) : base(myContext)
        {
            this.context = myContext;
            this._configuration = configuration;
        }

        public IEnumerable<ExpenseHistory> GetHistory(int expenseid)
        {
            var expense = from a in context.ExpenseHistories where a.ExpenseId == expenseid
                          select new ExpenseHistory()
                          {
                              Message = a.Message
                          };

            return expense.ToList();
        }

    }
}
