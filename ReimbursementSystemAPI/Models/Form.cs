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
        public DateTime? Receipt_Date { get; set; }
        public DateTime? Start_Date { get; set; }
        public DateTime? End_Date { get; set; }
        public Category? Category { get; set; }
        public string Payee { get; set; }
        public string Destination { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public float? Total { get; set; }
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
