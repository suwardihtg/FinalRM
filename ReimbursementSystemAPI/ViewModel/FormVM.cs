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
        public DateTime? Receipt_Date { get; set; }
        public DateTime? Start_Date { get; set; }
        public DateTime? End_Date { get; set; }
        public int Category { get; set; }
        public string Type { get; set; }
        public string Payee { get; set; }
        public string Description { get; set; }
        public float? Total { get; set; }
        public int ExpenseId { get; set; }
        public string Attachments { get; set; }
    }

}
