using ReimbursementSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Repository.Data
{
    public class JobRepository : GeneralRepository<MyContext, Job, int>
    {
        public JobRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
