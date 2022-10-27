using ReimbursementSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Models
{
    public class Form
    {
        [Key]
        public int FormId { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Category? Category { get; set; }
        public string Description { get; set; }
        public float? Total { get; set; }
        public int AccountNumber { get; set; }
        public string BankName { get; set; }
        public int Attachments { get; set; }

        [JsonIgnore]
        public virtual Expense Expenses { get; set; }
        public int ExpenseId { get; set; }
    }

    public enum Category
    {
        Transportation,
        Parking,
        Medical,
        Lodging
    }

}
