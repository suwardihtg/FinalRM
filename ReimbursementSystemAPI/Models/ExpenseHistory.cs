using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Models
{
    public class ExpenseHistory
    {
        [Key]
        public int HistoryId { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }

        [JsonIgnore]
        public virtual Expense Expenses { get; set; }
        public int ExpenseId { get; set; }
    }
}
