using ReimbursementSystemAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Models
{
    public class Expense
    {
        [Key]
        public int ExpenseId { get; set; }
        public Status Status { get; set; }
        public DateTime? SubmittedDate { get; set; }
        public string CommentManager { get; set; }
        public string CommentFinance { get; set; }
        public float? Total { get; set; }

        [JsonIgnore]
        public virtual Employee Employees { get; set; }
        public string EmployeeId { get; set; }

        [JsonIgnore]
        public virtual ICollection<Form> Forms { get; set; }

        [JsonIgnore]
        public virtual ICollection<ExpenseHistory> ExpenseHistories { get; set; }
    }

    public enum Status
    {
        Draft,
        Submitted,
        Canceled,
        ApprovedByManager,
        ApprovedByFinance,
        RejectedByManager,
        RejectedByFinance,
        Paid
    }

}
