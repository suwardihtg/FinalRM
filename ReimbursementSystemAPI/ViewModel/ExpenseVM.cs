using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.ViewModel
{
    public class ExpenseVM
    {
        public int ExpenseId { get; set; }
        public DateTime? Submitted { get; set; }
        public int Status { get; set; }
        public string Approver { get; set; }
        public string Description { get; set; }
        public string CommentManager { get; set; }
        public string CommentFinace { get; set; }
        public string Purpose { get; set; }
        public float? Total { get; set; }

        public string EmployeeId { get; set; }
    }
}
