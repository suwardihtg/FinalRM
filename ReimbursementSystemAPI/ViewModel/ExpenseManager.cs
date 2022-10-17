using System;
namespace ReimbursementSystemAPI.ViewModel
{
    public class ExpenseManager
    {
        public int ExpenseId { get; set; }
        public DateTime? DateTime { get; set; }
        public float? Total { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Purpose { get; set; }

    }
}
