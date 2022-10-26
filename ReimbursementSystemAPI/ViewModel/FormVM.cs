using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.ViewModel
{
    public class FormVM
    {
        public int FormId { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Category { get; set; }
        public string Description { get; set; }
        public float? Total { get; set; }
        public int AccountNumber { get; set; }
        public int ExpenseId { get; set; }
        public string Attachments { get; set; }
    }

}
